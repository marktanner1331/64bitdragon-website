-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 21, 2018 at 11:21 AM
-- Server version: 10.1.36-MariaDB
-- PHP Version: 7.0.32

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `learn`
--

-- --------------------------------------------------------

--
-- Table structure for table `articlebllobkeywords`
--

CREATE TABLE `articlebllobkeywords` (
  `ArticleBllobKeywordsID` int(11) NOT NULL,
  `ArticleBlobID` int(11) NOT NULL,
  `Keyword` varchar(32) CHARACTER SET ascii NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `articlebllobkeywords`
--

INSERT INTO `articlebllobkeywords` (`ArticleBllobKeywordsID`, `ArticleBlobID`, `Keyword`) VALUES
(1, 1, 'Arithmetic Mean'),
(2, 1, 'Mean'),
(3, 4, 'Matrix'),
(4, 4, 'numpy'),
(6, 4, 'python'),
(7, 4, 'LaTeX'),
(8, 5, 'Matrix'),
(9, 5, 'numpy'),
(11, 5, 'LaTeX'),
(12, 5, 'Python');

-- --------------------------------------------------------

--
-- Table structure for table `articleblob`
--

CREATE TABLE `articleblob` (
  `ArticleBlobID` int(11) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Path` varchar(128) NOT NULL,
  `Type` int(11) NOT NULL,
  `Keywords` varchar(64) NOT NULL,
  `Description` varchar(128) NOT NULL,
  `Finished` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `articleblob`
--

INSERT INTO `articleblob` (`ArticleBlobID`, `Name`, `Path`, `Type`, `Keywords`, `Description`, `Finished`) VALUES
(1, 'Mean', 'mathematics/statistics/Mean', 2, 'Arithmetic Mean,Mean', 'Find the average of a set of numbers.', 1),
(2, 'Mean Absolute Error', 'mathematics/statistics/MeanAbsoluteError', 2, '', 'Determine the error between two sets using the Mean Absolute Error', 1),
(3, 'Root Mean Squared Error', 'mathematics/statistics/RootMeanSquaredError', 2, '', 'Determine the error between two sets using the Root Mean Squared Error.', 1),
(4, 'Matrices', 'mathematics/linearAlgebra/matrix', 3, 'Matrix,numpy,python,LaTeX', 'Introduction to Matrices, with examples in Python and LaTeX', 1),
(5, 'Elements In A Matrix', 'mathematics/linearAlgebra/ElementsInAMatrix', 3, 'Matrix,numpy,LaTeX,Python', 'How to refer to elements in a matrix, with examples in python and LaTeX', 1),
(6, 'Matrix Transposition', 'mathematics/linearAlgebra/MatrixTransposition', 3, 'Matrix,numpy,python,transpose', 'Introduction to matrix transposition, including example code written in python', 1),
(7, 'Vectors', 'mathematics/linearAlgebra/Vectors', 3, 'Matrix,numpy,python,vectors', 'Introduction to vectors, including example code in Python', 1),
(8, 'Symmetric Matrices', 'mathematics/linearAlgebra/SymmetricMatrix', 3, 'matrix, symmetric matrix', 'Introduction to symmetric matrices.', 1),
(10, 'Diagonal and Scalar Matrices', 'mathematics/linearAlgebra/DiagonalAndScalarMatrices', 3, 'matrix, diagonal matrix, scalar matrix', 'Introduction to diagonal and scalar matrices', 1),
(11, 'Identity Matrices', 'mathematics/linearAlgebra/IdentityMatrix', 3, 'matrix,identity matrix,numpy', 'Introduction to the identity matrix, including example code in python', 1),
(12, 'Matrix Addition', 'mathematics/linearAlgebra/MatrixAddition', 3, 'matrix,addition,subtraction,numpy', 'An introduction to matrix addition and subtraction with example code in Python', 1),
(13, 'Scalar Matrix Multiplication', 'mathematics/linearAlgebra/ScalarMatrixMultiplication', 3, 'matrix,scalar,multiplication,numpy', 'An introduction to Scalar Matrix Multiplication with example code in Python', 1),
(14, 'Dot Product', 'mathematics/linearAlgebra/DotProduct', 3, 'vector,dot product,numpy', 'An introduction to the dot product, including example code in Python.', 1),
(15, 'Matrix Multiplication', 'mathematics/linearAlgebra/MatrixMultiplication', 3, 'matrix,matrix multiplication,numpy', 'An introduction to matrix multiplication, including example code in Python.', 1),
(16, 'Vector Outer Product', 'mathematics/linearAlgebra/VectorOuterProduct', 3, 'vector,outer product,numpy', 'Introduction to the vector outer product, including example code in Python.', 1),
(17, 'Kahan Summation', 'computerScience/numberAnalysis/KahanSummation', 5, 'kahan summation,numerical analysis,python', 'An introduction to Kahan Summation with example code in Python', 1),
(19, 'Sets', 'mathematics/setTheory/Sets', 6, 'set theory,LaTeX,python', 'Introduction to sets, with example code in Python', 1),
(20, 'Ordered Pairs', 'mathematics/setTheory/OrderedPairs', 6, 'set theory,ordered pair,latex,python', 'An introduction to Ordered Pairs, with example code in Python.', 1),
(21, 'Functions', 'mathematics/setTheory/Functions', 6, 'set theory,latex,functions,python', 'An introduction to functions, including example code in Python.', 1),
(22, 'Injections, Surjections, and Bijections', 'mathematics/setTheory/InjectionSurjectionBijection', 6, 'set theory,injection,surjection,bijection', 'An introduction to Injections, Surjections, and Bijections', 1),
(23, 'Inverse Functions', 'mathematics/setTheory/InverseFunctions', 6, 'set theory,latex,functions,inverse', 'An introduction to inverse functions.', 1),
(24, 'One-way Functions', 'computerScience/cryptography/OneWayFunctions', 7, 'one-way functions,cryptography,python', 'An introduction to One-way Functions, including example code in Python.', 1),
(25, 'Trapdoor Functions', 'computerScience/cryptography/TrapDoorFunctions', 7, 'trapdoor functions,cryptography,python', 'An introduction to Trapdoor Functions, including example code in Python.', 1),
(26, 'Important Sets', 'mathematics/setTheory/ImportantSets', 6, 'set theory,integers,real numbers,latex,python', 'An introduction to the set of integers and the set of real numbers, including example code in Python.', 1),
(27, 'Sets of Ordered Pairs', 'mathematics/setTheory/SetsOfOrderedPairs', 6, 'set theory,integers,real numbers,ordered pairs,latex', 'An introduction to the notation for sets of ordered pairs of integers and real numbers.', 1),
(28, 'Binary And Text data', 'computerScience/dataEncoding/BinaryAndText', 8, 'binary,text,python', 'An Introduction to the difference between binary and text data, including example code in Python.', 1),
(29, 'Base64', 'computerScience/dataEncoding/Base64', 8, 'base64,decode,encode,python', 'An introduction to how the Base64 encoding algorithm works, with example code in Python.', 0),
(30, 'Bitwise Operations', 'computerScience/dataEncoding/BitwiseOperations', 8, 'bitwise,shift,shifting,mask,bitmask,or,and,xor,bytes,python', 'An introduction to Bit Manipulation and Bitwise Operations, with example code in Python.', 1),
(31, 'Number Formats', 'computerScience/dataEncoding/NumberFormats', 8, 'binary,octal,decimal,hex,hexadecimal,number format,python', 'An introduction to different number formats from binary up to hexadecimal, including example code in Python.', 0),
(32, 'Representing Negative Numbers', 'computerScience/dataEncoding/NagativeNumbers', 8, 'twos compliment,ones complement,binary,negative,python', 'An introduction to representing negative numbers in binary, including example code in Python.', 0),
(33, 'Endianness', 'computerScience/dataEncoding/Endianness', 8, 'big endian,little endian,endianness,python', 'An introduction to endianness and how intergers are stored, including example code in Python', 0),
(34, 'Bit Shifting', 'computerScience/dataEncoding/BitShifting', 8, 'bit shifting,left shift,right shift,python', 'An introduction to bit shifting, including example code in Python.', 0),
(35, 'Divisibility', 'mathematics/numberTheory/Divisibility', 9, 'number theory,divisibility,latex,python', 'An introduction to the theory of Divisibility, including example code in Python.', 1),
(36, 'Divisibility Properties', 'mathematics/numberTheory/DivisibilityProperties', 9, 'number theory,divisibility,properties,latex,python', 'An overview of the properties of divisibility, including example code in Python.', 0);

-- --------------------------------------------------------

--
-- Table structure for table `articleblobtype`
--

CREATE TABLE `articleblobtype` (
  `ArticleBlobTypeID` int(11) NOT NULL,
  `ParentTypeID` int(11) NOT NULL,
  `Name` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `articleblobtype`
--

INSERT INTO `articleblobtype` (`ArticleBlobTypeID`, `ParentTypeID`, `Name`) VALUES
(1, 0, 'Mathematics'),
(2, 1, 'Statistics'),
(3, 1, 'Linear Algebra'),
(4, 0, 'Computer Science'),
(5, 4, 'Number Analysis'),
(6, 1, 'Set Theory'),
(7, 4, 'Cryptography'),
(8, 4, 'Data Encoding'),
(9, 1, 'Number Theory');

-- --------------------------------------------------------

--
-- Table structure for table `blobprerequisite`
--

CREATE TABLE `blobprerequisite` (
  `BlobPrerequisiteID` int(11) NOT NULL,
  `ParentBlobID` int(11) NOT NULL,
  `ChildBlobID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `blobprerequisite`
--

INSERT INTO `blobprerequisite` (`BlobPrerequisiteID`, `ParentBlobID`, `ChildBlobID`) VALUES
(1, 2, 1),
(3, 3, 2),
(6, 3, 1),
(7, 5, 4),
(8, 6, 4),
(9, 7, 4),
(10, 7, 6),
(11, 8, 0),
(12, 8, 4),
(13, 8, 6),
(14, 10, 8),
(15, 11, 10),
(16, 12, 4),
(17, 12, 5),
(18, 13, 4),
(19, 14, 7),
(20, 15, 4),
(21, 15, 14),
(22, 16, 7),
(23, 20, 19),
(24, 21, 19),
(25, 21, 20),
(26, 22, 21),
(27, 23, 21),
(28, 23, 22),
(29, 25, 24),
(30, 25, 27),
(31, 27, 26),
(32, 27, 20),
(33, 26, 19),
(34, 24, 21),
(35, 24, 23),
(36, 29, 28),
(37, 29, 30),
(39, 29, 34);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `articlebllobkeywords`
--
ALTER TABLE `articlebllobkeywords`
  ADD PRIMARY KEY (`ArticleBllobKeywordsID`),
  ADD KEY `Keyword` (`Keyword`);

--
-- Indexes for table `articleblob`
--
ALTER TABLE `articleblob`
  ADD PRIMARY KEY (`ArticleBlobID`);

--
-- Indexes for table `articleblobtype`
--
ALTER TABLE `articleblobtype`
  ADD PRIMARY KEY (`ArticleBlobTypeID`);

--
-- Indexes for table `blobprerequisite`
--
ALTER TABLE `blobprerequisite`
  ADD PRIMARY KEY (`BlobPrerequisiteID`),
  ADD KEY `ParentBlobID` (`ParentBlobID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `articlebllobkeywords`
--
ALTER TABLE `articlebllobkeywords`
  MODIFY `ArticleBllobKeywordsID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `articleblob`
--
ALTER TABLE `articleblob`
  MODIFY `ArticleBlobID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT for table `articleblobtype`
--
ALTER TABLE `articleblobtype`
  MODIFY `ArticleBlobTypeID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `blobprerequisite`
--
ALTER TABLE `blobprerequisite`
  MODIFY `BlobPrerequisiteID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=40;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
