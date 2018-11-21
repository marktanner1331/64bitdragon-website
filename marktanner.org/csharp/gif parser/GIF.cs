using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gif_parser
{
    class GIF : ByteArray
    {
        private string version;
        public ushort width;
        public ushort height;
        private bool GlobalColorTableIsUsed;
        private byte colorResolution;
        private bool globalColorTableIsOrdered;
        private uint globalColorTableSize; // (number of entries, not size in bytes)
        private byte backgroundColorIndex;
        public double pixelAspectRatio;
        private Color[] globalColorTable; //rgb stored as 3 bytes, (the first byte is empty)
        public bool loopAnimation = false;
        public ushort numLoops; //0 means infinite

        public List<GIFImage> images = new List<GIFImage>();

        //used for lzw
        private byte[] block;

        public GIF(byte[] data) : base(data)
        {
            parseGIF();
        }

        /// <summary>
        /// parses basic data out of the gif like the width and height
        /// </summary>
        private void parseGIF()
        {
            //check the sig, stored in the first 6 bytes
            string sig = getString(6);
            switch(sig)
            {
                case "GIF89a":
                    version = "89a";
                    break;
                case "GIF87a":
                    version = "87a";
                    break;
                default:
                    version = "unknown";
                    throw new Exception("unknown gif version: " + sig);
            }

            //its time to parse the logical screen descriptor

            //get the width and height, stored in 2 bytes each
            width = getUI16();
            height = getUI16();

            //now for the flags
            byte flags = getByte();

            //check if its using a global color table
            GlobalColorTableIsUsed = (flags & 1) == 1;

            //calculate the color resolution
            colorResolution = 1;
            if ((flags >> 1 & 1) == 1) colorResolution += 1;
            if ((flags >> 2 & 1) == 1) colorResolution += 2;
            if ((flags >> 3 & 1) == 1) colorResolution += 4;

            //check if the global color table is ordered
            globalColorTableIsOrdered = (flags >> 4 & 1) == 1;

            //finally the size of the global color table
            byte tableExponent = 1;
            if ((flags >> 5 & 1) == 1) tableExponent += 1;
            if ((flags >> 6 & 1) == 1) tableExponent += 2;
            if ((flags >> 7 & 1) == 1) tableExponent += 4;
            globalColorTableSize = (uint)Math.Pow(2, (double)tableExponent);

            //the index of the background color in teh global color table
            backgroundColorIndex = getByte();

            //the aspect ratio for the pixels, will be 0 (square) in most cases.
            //google pixel aspect ratio for more info
            byte aspectByte = getByte();
            if (aspectByte == 0)
            {
                pixelAspectRatio = 1;
            }
            else
            {
                pixelAspectRatio = (aspectByte + 15) / 64;
            }

            //its time to parse the global color table (if there is one)
            //hint: if there is a global color table, individual images dont have a local color table

            if (GlobalColorTableIsUsed)
            {
                //initialize the global color table
                globalColorTable = new Color[globalColorTableSize];

                //now we can run through the entries
                for (int i = 0; i < globalColorTableSize; i++)
                {
                    globalColorTable[i] = Color.FromArgb(getByte(), getByte(), getByte());
                }
            }

            //its time to parse the image data, which are a series of blocks ending in a trailer (the 0x3b)
            byte extensionIntroducer;
            while((extensionIntroducer = getByte()) != 0x3b)
            {
                //the extension introducer tells us what type of byte it is
                switch (extensionIntroducer)
                {
                    case 0x2c: //we have found an image block
                        parseImageBlock();
                        break;
                    case 0x21: //we have found an extension block
                        //there are different types of extension blocks, so the next byte contains
                        //a label describing the type
                        byte label = getByte();
                        switch (label)
                        {
                            case 0xf9: //we have found a graphic control block
                                parseGraphicControlBlock();
                                break;
                            case 0xfe: //we have found a comment block
                                parseCommentBlock();
                                break;
                            case 0x01: //we have found a plain text block
                                //error handling
                                break;
                            case 0xff: //we have found an application block
                                parseApplicationBlock();
                                break;
                            default: //we have an unknown block, 
                                break;
                        }
                        break;
                    default: //we have found an unknown block
                        //error handling
                        break;
                }
            }
        }

        /// <summary>
        /// when a comment is encountered in the stream, control is passed to this method
        /// </summary>
        private void parseCommentBlock()
        {
            //get the size of the comment
            byte commentSize = getByte();

            //get the comment
            string comment = getString(commentSize);

            //parse the terminating character
            if (getByte() != 0x00)
            {
                //error handling
            }
        }

        private void parseImageBlock()
        {
            GIFImage image;

            //image was added by graphic control extension
            if (images[images.Count() - 1].hasData == false)
            {
                image = images[images.Count() - 1];
            }
            else
            {
                image = new GIFImage();
                images.Add(image);
            }

            image.hasData = true;

            //get the left, top, width and height of the image
            image.left = getUI16();
            image.top = getUI16();
            image.width = getUI16();
            image.height = getUI16();

            //next is a bunch of flags used for the image
            byte flags = getByte();
            bool hasLocalColorTable = (flags & 1) == 1;
            bool interlace = (flags >> 1 & 1) == 1;
            if (interlace)
            {
                //error handling
            }

            bool sort = (flags >> 2 & 1) == 1;

            //might not be needed
            Color[] localColorTable = null;

            //get the size of the local color table
            if (hasLocalColorTable)
            {
                byte tableExponent = 1;
                if ((flags >> 5 & 1) == 1) tableExponent += 1;
                if ((flags >> 6 & 1) == 1) tableExponent += 2;
                if ((flags >> 7 & 1) == 1) tableExponent += 4;
                uint localColorTableSize = (uint)Math.Pow(2, (double)tableExponent);

                //initialize the global color table
                localColorTable = new Color[localColorTableSize];

                //now we can run through the entries
                for (int a = 0; a < localColorTableSize; a++)
                {
                    localColorTable[a] = Color.FromArgb(getByte(), getByte(), getByte());
                }
            }

            //decode the image and store the indices
            //Adapted from John Cristy's ImageMagick.

            int[] tableIndices = new int[width * height];

            int MaxStackSize = 4096; //2^12

		    // LZW decoder working arrays
            int[] prefix;
            int[] suffix;
            int[] pixelStack;
            
            int NullCode = -1;
			int npix = image.width * image.height;
			int available;
			int clear;
			int code_mask; //applied to datum to only get the bits we want 
			int code_size;
			int end_of_information;
			int in_code;
			int old_code; //stores the code that was read before the current one
			int bits; //number of bits read when reading the current code
			int code;//stores the current code
			int count; //stores the number of bytes left to be read in the current block
			int i;
			int datum; //stores the current code + a few extra bits (apply mask to get code)
			int data_size; //initial number of bits
			int first; //stores the first code in the block that was read (or the first code after a clear)
            int stackPointer; //stackPointer is used to refer to the top of the pixel stack
			int bi; //index of the current byte
            int pi;

            prefix = new int[MaxStackSize];
            suffix = new int[MaxStackSize];

            pixelStack = new int[MaxStackSize + 1];

            data_size = getByte(); //get the initial number of bits
            clear = 1 << data_size; //same as 2^data_size
            end_of_information = clear + 1;
            available = clear + 2; //first free index in the dictionary alphabet + clear + end_of_information
            old_code = NullCode;
            code_size = data_size + 1;
            code_mask = (1 << code_size) - 1; //causes code_mask to be full of 1's. 

            for (code = 0; code < clear; code++) 
			{
				prefix[code] = 0;
				
				//initialize the dictionary with the alphabet.
				suffix[code] = code;
			}

            //  Decode GIF pixel stream.
            datum = bits = count = first = stackPointer = pi = bi = 0;

            for (i = 0; i < npix;) 
			{
                if (stackPointer == 0) 
				{
					if (bits < code_size) 
					{
						//  Load bytes until there are enough bits for a code.
						if (count == 0) 
						{
							// Read a new data block.
							count = readBlock();
							if (count <= 0)
								break;
							bi = 0;
						}
						//needed for codes with more than 8 bits
						datum += ((int)(block[bi]) & 0xff) << bits;
						
						bits += 8;
						bi++;
						count--;
						continue;
					}
					//if we have reached here, datum contains enough bits to read the code
					
					//  Get the next code.
					
					//apply the mask to datum to get only the code
					code = datum & code_mask;
					
					//remove the code from datum, leaving the bits that havent been read
					datum >>= code_size;
					
					//reflex this change in the current bit count
					bits -= code_size;
					
					//  Interpret the code
					if ((code > available) || (code == end_of_information))
						break;
					if (code == clear) 
					{
						//  Reset decoder.
						code_size = data_size + 1;
						code_mask = (1 << code_size) - 1;
						available = clear + 2;
						old_code = NullCode;
						continue;
					}
					
					//this is true when we are reading the first byte, or if a clear is encountered
					if (old_code == NullCode) 
					{
						//get the data out of the dictionary, and add it to the pixel stack
						pixelStack[stackPointer++] = suffix[code];
						
						//set the last code and first code to this code
						old_code = code;
						first = code;
						continue;
					}
					
					//if its not the first code, we have hit here
					in_code = code;
					
					//if this is true, we have found a code that isnt in the dictionary
					if (code == available) 
					{
						pixelStack[stackPointer++] = first;
						code = old_code;
					}
					//this loop adds data to the pixel stack for multiple character dictionary entries
					while (code > clear) 
					{
						pixelStack[stackPointer++] = suffix[code];
						code = prefix[code];
					}
					//get the least significant byte of the element in the dictionary and store it in first
					first = (suffix[code]) & 0xff;

					//  Add a new string to the string table,
			
					if (available >= MaxStackSize) break;
					pixelStack[stackPointer++] = first;
					prefix[available] = old_code;
					suffix[available] = first;
					available++;
					if (((available & code_mask) == 0)
						&& (available < MaxStackSize)) 
					{
						code_size++;
						code_mask += available;
					}
					old_code = in_code;
				}

				//  Pop a pixel off the pixel stack.

				stackPointer--;
                tableIndices[pi++] = pixelStack[stackPointer];
				i++;
			}

            for (i = pi; i < npix; i++) 
			{
				tableIndices[i] = 0; // clear missing pixels
			}
            
            //convert the table full of indices, to the table full of colours
            Color[] colorTableToUse = null;
            if (hasLocalColorTable)
            {
                colorTableToUse = localColorTable;
            }
            else if (GlobalColorTableIsUsed)
            {
                colorTableToUse = globalColorTable;
            }
            else
            {
                //error handling
            }

            image.pixels = new Color[npix];
            for (int j = 0; j < npix; j++)
            {
                image.pixels[j] = colorTableToUse[tableIndices[j]];
            }
            
            //finally we parse the termination block
            if (getByte() != 0x00)
            {
                throw new Exception("unexpected end of image block");
            }
        }

        /// <summary>
        /// reads a block from the image data and return the size
        /// </summary>
        /// <returns></returns>
        private int readBlock()
		{
			byte blockSize = getByte();
			int n = 0;
			if (blockSize > 0) 
			{
				try 
				{
					while (n < blockSize) 
					{
                        block = getBytesAsArray((uint)(blockSize - n));
						if ( (blockSize - n) == -1) 
							break;
						n += (blockSize - n);
					}
				} 
				catch (Exception) 
				{
				}

				if (n < blockSize) 
				{
					//error handling
				}
			}
			return n;
		}

        /// <summary>
        /// when a stream encounters an image control block, control is given to this function
        /// to parse it
        /// </summary>
        private void parseGraphicControlBlock()
        {
            GIFImage image = new GIFImage();
            images.Add(image);

            //the block size should be 0x04
            byte blockSize = getByte();

            if (blockSize == 0x04)
            {
                //several settings are stored as flags in a single byte
                byte flags = getByte();

                //parse the disposal method
                image.disposalMethod = 0;
                if ((flags >> 3 & 1) == 1) image.disposalMethod += 1;
                if ((flags >> 4 & 1) == 1) image.disposalMethod += 2;
                if ((flags >> 5 & 1) == 1) image.disposalMethod += 4;

                //the user input flag
                image.userInput = (flags >> 6 & 1) == 1;

                //the transparency color flag
                image.transparencyColorFlag = (flags >> 7 & 1) == 1;

                //next we parse the delay, stored as 1/100th's of a second
                image.delay = getUI16();

                //not completely sure how the transparency index works, although if the transparency color flag
                //is not set, then it should be skipped
                image.transparencyIndex = getByte();

                //finally its the block terminator, should be 0x00
                if (getByte() != 0x00)
                {
                    //error handling
                }
            }
            else
            {
                //error handling
            }
        }

        /// <summary>
        /// when an application block is found in the byte stream, control is passed to this function
        /// in order for it to be parsed
        /// </summary>
        private void parseApplicationBlock()
        {
            //the block size is always 11, if its not then something is wrong
            byte blockSize = getByte();
            if (blockSize != 0x0b)
            {
                //error handling
            }

            //next is the application identifiers, (its an 8 character string)
            string applicationID = getString(8);

            //i think that the authentication code is used so that only the application that
            //created the extension can parse it.
            byte[] authenticationCode = getBytesAsArray(3);

            //as the data at this point depends on the application that created it, we can switch on the
            //applicationID
            switch (applicationID)
            {
                case "NETSCAPE":
                    parseNetscapeApplicationExtension();
                    break;
                default:
                    //if i dont understand an application extension, it needs to be skipped, this
                    //is a bit troublesome
                    //keep going until we hit a null byte, this is hopefully the terminator
                    while (getByte() != 0x00)
                    {

                    }

                    //although sometimes there will be an extra null byte or other random data.
                    //skip these until we find the start of a block
                    byte randomByte = getByte();
                    while (randomByte != 0x21 && randomByte != 0x2c)
                    {
                        randomByte = getByte();
                    }

                    //now we need to go back two bytes so that the rest of the stream can be parsed properly
                    Position -= 2;
                    trace("unknown application extension block: " + applicationID);
                    break;
            }

            //finally there will be a null byte which acts as a block terminator
            byte terminator = getByte();
            if (terminator != 0x00)
            {
                //error handling
            }
        }

        /// <summary>
        /// the byte stream is at a point where it needs to parse the netscape application extension
        /// control is handede to this method to achieve this
        /// </summary>
        private void parseNetscapeApplicationExtension()
        {
            //get the sub block size, this should always be 3
            byte subBlockSize = getByte();
            if (subBlockSize == 0x03)
            {
                //get the sub block id
                byte subBlockID = getByte();
                switch (subBlockID)
                {
                    case 0x01: //we have a looping animation sub block
                        loopAnimation = true;
                        numLoops = getUI16();
                        break;
                    default:
                        //error handling
                        break;
                }
            }
            else
            {
                //error handling
            }
        }

        private void trace(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
