-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost
-- Généré le :  lun. 06 sep. 2021 à 02:32
-- Version du serveur :  5.7.26
-- Version de PHP :  7.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `royaume_du_hibou`
--
DROP DATABASE IF EXISTS `royaume_du_hibou`;
CREATE DATABASE IF NOT EXISTS `royaume_du_hibou` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `royaume_du_hibou`;

-- --------------------------------------------------------

--
-- Structure de la table `address`
--

CREATE TABLE `address` (
  `id_address` int(10) UNSIGNED NOT NULL,
  `address` varchar(100) NOT NULL,
  `city_id` int(11) UNSIGNED NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `address`
--

INSERT INTO `address` (`id_address`, `address`, `city_id`) VALUES
(1, 'Anonyme', 1),
(2, '1 Privet Drive', 2),
(3, '2 Privet Drive', 2),
(4, '3 Privet Drive', 2),
(5, '4 Privet Drive', 2),
(6, '5 Privet Drive', 2),
(7, '6 Privet Drive', 2),
(8, '7 Privet Drive', 2),
(9, '8 Privet Drive', 2);

-- --------------------------------------------------------

--
-- Structure de la table `article`
--

CREATE TABLE `article` (
  `id_article` int(10) UNSIGNED NOT NULL,
  `name` varchar(100) NOT NULL,
  `ean_code` varchar(50) NOT NULL,
  `brand_id` int(10) UNSIGNED NOT NULL,
  `category_id` int(10) UNSIGNED NOT NULL,
  `sub_category_id` int(10) UNSIGNED DEFAULT NULL,
  `price_info_id` int(10) UNSIGNED NOT NULL,
  `quantity` int(11) NOT NULL,
  `deposit` tinyint(1) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `article`
--

INSERT INTO `article` (`id_article`, `name`, `ean_code`, `brand_id`, `category_id`, `sub_category_id`, `price_info_id`, `quantity`, `deposit`, `date`) VALUES
(1, 'Baguette de Cho Chang', '978020137964', 1, 1, 1, 1, 11, 0, '2021-09-06 03:33:13'),
(2, 'Baguette du professeur Severus Rogue', '978020137966', 1, 1, 1, 2, 46, 0, '2021-09-06 03:33:13'),
(3, 'Baguette du professeur Pomona Chourave', '978020137967', 1, 1, 1, 3, 25, 0, '2021-09-06 03:33:13'),
(4, 'Baguette du professeur Minerva McGonagall', '978020137968', 1, 1, 1, 4, 12, 0, '2021-09-06 03:33:13'),
(5, 'Baguette du professeur Albus Dumbledore - Baguette de sureau', '978020137971', 1, 1, 1, 5, 40, 0, '2021-09-06 03:33:13'),
(6, 'Baguette de Sirius Black', '978020137976', 1, 1, 1, 6, 4, 0, '2021-09-06 03:33:13'),
(7, 'Baguette de Ron Weasley', '978020137980', 1, 1, 1, 7, 42, 0, '2021-09-06 03:33:13'),
(8, 'Baguette de Remus Lupin', '978020137981', 1, 1, 1, 8, 22, 0, '2021-09-06 03:33:13'),
(9, 'Baguette de Neville Londubat', '978020137988', 1, 1, 1, 9, 29, 0, '2021-09-06 03:33:13'),
(10, 'Baguette de Lord Voldemort', '978020138001', 1, 1, 1, 10, 41, 0, '2021-09-06 03:33:13'),
(11, 'Baguette de Hermionne Granger', '978020138006', 1, 1, 1, 11, 16, 0, '2021-09-06 03:33:13'),
(12, 'Baguette de Harry Potter', '978020138007', 1, 1, 1, 12, 11, 0, '2021-09-06 03:33:13'),
(13, 'Baguette de Grindelwald', '978020138008', 1, 1, 1, 13, 18, 0, '2021-09-06 03:33:13'),
(14, 'Baguette de Ginny Weasley', '978020138011', 1, 1, 1, 14, 15, 0, '2021-09-06 03:33:13'),
(15, 'Baguette de Fenrir Greyback', '978020138013', 1, 1, 1, 15, 18, 0, '2021-09-06 03:33:13'),
(16, 'Baguette de Drago Malefoy', '978020138014', 1, 1, 1, 16, 8, 0, '2021-09-06 03:33:13'),
(17, 'Baguette de Bellatrix Lestrange', '978020138018', 1, 1, 1, 17, 40, 0, '2021-09-06 03:33:13'),
(18, 'Réplique du parapluie de Rubeus Hagrid', '978020138023', 1, 1, 2, 18, 37, 0, '2021-09-06 03:33:13'),
(19, 'Boîte Ollivander Sirius Black', '978020138024', 1, 1, 2, 19, 16, 0, '2021-09-06 03:33:13'),
(20, 'Boîte Ollivander Voldemort', '978020138025', 1, 1, 2, 20, 21, 0, '2021-09-06 03:33:13'),
(21, 'Boîte Ollivander Severus Rogue', '978020138026', 1, 1, 2, 21, 15, 0, '2021-09-06 03:33:13'),
(22, 'Boîte Ollivander Drago Malefoy', '978020138027', 1, 1, 2, 22, 9, 0, '2021-09-06 03:33:13'),
(23, 'Boîte Ollivander Albus Dumbledore', '978020138028', 1, 1, 2, 23, 33, 0, '2021-09-06 03:33:13'),
(24, 'Boîte Ollivander Hermionne Granger', '978020138029', 1, 1, 2, 24, 44, 0, '2021-09-06 03:33:13'),
(25, 'Boîte Ollivander Ron Weasley', '978020138030', 1, 1, 2, 25, 12, 0, '2021-09-06 03:33:13'),
(26, 'Boîte Ollivander Harry Potter', '978020138031', 1, 1, 2, 26, 35, 0, '2021-09-06 03:33:14'),
(27, 'Canne Baguette - Lucius Malefoy', '978020138032', 1, 1, 3, 27, 31, 0, '2021-09-06 03:33:14'),
(28, 'Présentoir 4 baguettes la carte du Maraudeur', '978020138033', 1, 1, 3, 28, 24, 0, '2021-09-06 03:33:14'),
(29, 'Présentoir 5 baguettes la marque des ténèbres', '978020138034', 1, 1, 3, 29, 1, 0, '2021-09-06 03:33:14'),
(30, 'Présentoir Serdaigle pour baguette', '978020138035', 1, 1, 3, 30, 24, 0, '2021-09-06 03:33:14'),
(31, 'Présentoir Poufsouffle pour baguette', '978020138036', 1, 1, 3, 31, 22, 0, '2021-09-06 03:33:14'),
(32, 'Présentoir Serpentard pour baguette', '978020138037', 1, 1, 3, 32, 20, 0, '2021-09-06 03:33:14'),
(33, 'Présentoir Gryffondor pour baguette', '978020138038', 1, 1, 3, 33, 21, 0, '2021-09-06 03:33:14'),
(34, 'Figurine toyllectible Harry Potter', '978020138041', 2, 2, NULL, 34, 36, 0, '2021-09-06 03:33:14'),
(35, 'Figurine toyllectible Hermionne Granger', '978020138042', 2, 2, NULL, 35, 30, 0, '2021-09-06 03:33:14'),
(36, 'Figurine toyllectible Albus Dumbledore', '978020138043', 2, 2, NULL, 36, 9, 0, '2021-09-06 03:33:14'),
(37, 'Figurine toyllectible Dobby', '978020138044', 2, 2, NULL, 37, 17, 0, '2021-09-06 03:33:14'),
(38, 'Réplique chocogrenouille', '978020138045', 3, 2, NULL, 38, 16, 0, '2021-09-06 03:33:14'),
(39, 'Dobby articulé', '978020138046', 3, 2, NULL, 39, 44, 0, '2021-09-06 03:33:14'),
(40, 'Lutin de Cornouailles articulé', '978020138047', 3, 2, NULL, 40, 2, 0, '2021-09-06 03:33:14'),
(41, 'Mandragore', '978020138048', 1, 2, 4, 41, 13, 0, '2021-09-06 03:33:14'),
(42, 'Aragog', '978020138049', 1, 2, 4, 42, 3, 0, '2021-09-06 03:33:14'),
(43, 'Lutin de Cornouailles', '978020138050', 1, 2, 4, 43, 36, 0, '2021-09-06 03:33:14'),
(44, 'Croûtard', '978020138051', 1, 2, 4, 44, 8, 0, '2021-09-06 03:33:15'),
(45, 'Touffu', '978020138052', 1, 2, 4, 45, 5, 0, '2021-09-06 03:33:15'),
(46, 'Niffleur', '978020138054', 1, 2, 4, 46, 39, 0, '2021-09-06 03:33:15'),
(47, 'Oiseau-Tonnerre', '978020138055', 1, 2, 4, 47, 31, 0, '2021-09-06 03:33:15'),
(48, 'Occamy', '978020138056', 1, 2, 4, 48, 28, 0, '2021-09-06 03:33:15'),
(49, 'Botruc', '978020138057', 1, 2, 4, 49, 19, 0, '2021-09-06 03:33:15'),
(50, 'Demiguise', '978020138058', 1, 2, 4, 50, 20, 0, '2021-09-06 03:33:15'),
(51, 'Sculpture Hedwige', '978020138059', 1, 2, 5, 51, 42, 0, '2021-09-06 03:33:15'),
(52, 'Hedwige miniature en cage', '978020138060', 1, 2, 5, 52, 43, 0, '2021-09-06 03:33:15'),
(53, 'Fumseck le Phénix', '978020138061', 1, 2, 5, 53, 4, 0, '2021-09-06 03:33:15'),
(54, 'Dobby', '978020138062', 1, 2, 6, 54, 22, 0, '2021-09-06 03:33:15'),
(55, 'Cavalier blanc - Echiquier des sorciers', '978020138064', 3, 2, 6, 55, 18, 0, '2021-09-06 03:33:15'),
(56, 'Cavalier noir - Echiquier des sorciers', '978020138065', 3, 2, 6, 56, 31, 0, '2021-09-06 03:33:15'),
(57, 'Poudlard Express', '978020138066', 3, 2, 6, 57, 12, 0, '2021-09-06 03:33:15'),
(58, 'Peluche Hedwige', '978020138067', 3, 3, 7, 58, 17, 0, '2021-09-06 03:33:16'),
(59, 'Peluche Pattenrond', '978020138068', 3, 3, 7, 59, 3, 0, '2021-09-06 03:33:16'),
(60, 'Peluche Croûtard', '978020138069', 3, 3, 7, 60, 27, 0, '2021-09-06 03:33:16'),
(61, 'Peluche interactive - Niffleur', '978020138070', 3, 3, 7, 61, 48, 0, '2021-09-06 03:33:16'),
(62, 'Peluche interactive - Dobby', '978020138071', 3, 3, 7, 62, 27, 0, '2021-09-06 03:33:16'),
(63, 'Peluche et coussin de la maison de Serdaigle', '978020138072', 3, 3, 8, 63, 40, 0, '2021-09-06 03:33:16'),
(64, 'Peluche et coussin de la maison de Poufsouffle', '978020138073', 3, 3, 8, 64, 20, 0, '2021-09-06 03:33:16'),
(65, 'Peluche et coussin de la maison de Serpentard', '978020138074', 3, 3, 8, 65, 14, 0, '2021-09-06 03:33:16'),
(66, 'Peluche et coussin de la maison de Gryffondor', '978020138075', 3, 3, 8, 66, 33, 0, '2021-09-06 03:33:16'),
(67, 'Stylo et porte stylo PoufSouffle', '978020138076', 3, 4, 9, 67, 18, 0, '2021-09-06 03:33:16'),
(68, 'Stylo et porte stylo Serdaigle', '978020138077', 3, 4, 9, 68, 0, 0, '2021-09-06 03:33:16'),
(69, 'Stylo et porte stylo Serpentard', '978020138078', 3, 4, 9, 69, 13, 0, '2021-09-06 03:33:17'),
(70, 'Stylo et porte stylo Gryffondor', '978020138079', 3, 4, 9, 70, 37, 0, '2021-09-06 03:33:17'),
(71, 'Collection de marque-pages horcruxes', '978020138084', 3, 4, 9, 71, 30, 0, '2021-09-06 03:33:17'),
(72, 'Journal de Tom Jedusor', '978020138085', 1, 4, 10, 72, 10, 0, '2021-09-06 03:33:17'),
(73, 'Journal - Serdaigle', '978020138086', 3, 4, 10, 73, 28, 0, '2021-09-06 03:33:17'),
(74, 'Journal - Poufsouffle', '978020138087', 3, 4, 10, 74, 18, 0, '2021-09-06 03:33:17'),
(75, 'Journal - Gryffondor', '978020138088', 3, 4, 10, 75, 34, 0, '2021-09-06 03:33:17'),
(76, 'Journal - Serpentard', '978020138089', 3, 4, 10, 76, 23, 0, '2021-09-06 03:33:17'),
(77, 'Ouvre-lettres épée de Gryffondor', '978020138090', 1, 4, 11, 77, 30, 0, '2021-09-06 03:33:17'),
(78, 'Porte-clés serpent de Serpentard', '978020138091', 1, 4, 12, 78, 15, 0, '2021-09-06 03:33:17'),
(79, 'Porte-clés blaireau de Poufsouffle', '978020138092', 1, 4, 12, 79, 49, 0, '2021-09-06 03:33:17'),
(80, 'Porte-clés corbeau de Serdaigle', '978020138093', 1, 4, 12, 80, 40, 0, '2021-09-06 03:33:18'),
(81, 'Porte-clés lion de Gryffondor', '978020138094', 1, 4, 12, 81, 32, 0, '2021-09-06 03:33:18'),
(82, 'Boucles d\'oreilles de bal - Hermionne', '978020138095', 1, 5, 13, 82, 11, 0, '2021-09-06 03:33:18'),
(83, 'Retourneur de temps', '978020138096', 1, 5, 14, 83, 33, 0, '2021-09-06 03:33:18'),
(84, 'Diadème de Rowena Serdaigle - Horcruxe', '978020138097', 1, 5, 15, 84, 3, 0, '2021-09-06 03:33:18'),
(85, 'Bague de Gaunt - Horcruxe', '978020138098', 1, 5, 15, 85, 48, 0, '2021-09-06 03:33:18'),
(86, 'Charm Hedwige', '978020138099', 3, 5, 16, 86, 41, 0, '2021-09-06 03:33:18'),
(87, 'Charm Eclair', '978020138100', 3, 5, 16, 87, 38, 0, '2021-09-06 03:33:18'),
(88, 'Charm Vif d\'or', '978020138101', 3, 5, 16, 88, 42, 0, '2021-09-06 03:33:19'),
(89, 'Lanterne de Rubeus Hagrid', '978020138103', 3, 6, NULL, 89, 37, 0, '2021-09-06 03:33:19'),
(90, 'Sac d\'Hermionne', '978020138104', 3, 6, NULL, 90, 36, 0, '2021-09-06 03:33:19'),
(91, 'Sablier du professeur Slughorn', '978020138105', 1, 6, NULL, 91, 39, 0, '2021-09-06 03:33:19'),
(92, 'Coupe de Dumbledore', '978020138106', 1, 6, NULL, 92, 35, 0, '2021-09-06 03:33:19'),
(93, 'Œuf d\'Or - Taille réelle', '978020138107', 1, 6, NULL, 93, 15, 0, '2021-09-06 03:33:19'),
(94, 'La Coupe de Cristal', '978020138108', 1, 6, NULL, 94, 28, 0, '2021-09-06 03:33:19'),
(95, 'Carte du Maraudeur', '978020138109', 1, 6, NULL, 95, 18, 0, '2021-09-06 03:33:19'),
(96, 'Réplique collector du balai Eclair de feu', '978020138110', 1, 6, NULL, 96, 47, 0, '2021-09-06 03:33:19'),
(97, 'La Coupe des 3 Sorciers', '978020138111', 1, 6, NULL, 97, 39, 0, '2021-09-06 03:33:20'),
(98, 'Echiquier du défi final', '978020138112', 1, 6, NULL, 98, 11, 0, '2021-09-06 03:33:20'),
(99, 'Puzzle - Carte du Maraudeur', '978020138113', 3, 7, NULL, 99, 39, 0, '2021-09-06 03:33:20'),
(100, 'Echiquier des sorciers', '978020138114', 3, 7, NULL, 100, 37, 0, '2021-09-06 03:33:20'),
(101, 'Baguette Harry etat moyen', 'DEP_00000001', 3, 1, 2, 101, 1, 1, '2021-09-06 03:57:45'),
(102, 'Peluche Nagini', 'DEP_00000002', 1, 3, 7, 102, 1, 1, '2021-09-06 03:59:20'),
(103, 'Sablier de Slughorn', 'DEP_00000003', 1, 6, NULL, 103, 1, 1, '2021-09-06 03:59:20'),
(104, 'Pièces Gringotts', 'DEP_00000004', 1, 6, NULL, 104, 1, 1, '2021-09-06 04:01:23'),
(105, 'Boule de la prophétie', 'DEP_00000005', 3, 6, NULL, 105, 1, 1, '2021-09-06 04:01:24'),
(106, 'Baguette Griffondor', 'DEP_00000006', 3, 1, 3, 106, 1, 1, '2021-09-06 04:03:02'),
(107, 'Baguette Serpentard', 'DEP_00000007', 3, 1, 3, 107, 1, 1, '2021-09-06 04:03:02'),
(108, 'Baguette Poufsouffle', 'DEP_00000008', 3, 1, 3, 108, 1, 1, '2021-09-06 04:03:02'),
(109, 'Baguette Serdaigle', 'DEP_00000009', 3, 1, 3, 109, 1, 1, '2021-09-06 04:03:02'),
(110, 'Retourneur de temps', 'DEP_00000010', 2, 5, NULL, 110, 1, 1, '2021-09-06 04:04:58');

-- --------------------------------------------------------

--
-- Structure de la table `brand`
--

CREATE TABLE `brand` (
  `id_brand` int(10) UNSIGNED NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `brand`
--

INSERT INTO `brand` (`id_brand`, `name`) VALUES
(2, 'BendyFigs'),
(1, 'Noble Collection'),
(3, 'Warner Bros');

-- --------------------------------------------------------

--
-- Structure de la table `category`
--

CREATE TABLE `category` (
  `id_category` int(10) UNSIGNED NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `category`
--

INSERT INTO `category` (`id_category`, `name`) VALUES
(1, 'Baguettes magiques'),
(5, 'Bijoux'),
(2, 'Figurines'),
(7, 'Jeux'),
(4, 'Papeterie'),
(3, 'Peluche'),
(6, 'Pièces de collection');

-- --------------------------------------------------------

--
-- Structure de la table `city`
--

CREATE TABLE `city` (
  `id_city` int(10) UNSIGNED NOT NULL,
  `postal_code` varchar(5) NOT NULL DEFAULT '',
  `name` varchar(50) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `city`
--

INSERT INTO `city` (`id_city`, `postal_code`, `name`) VALUES
(1, '', ''),
(2, '4444', 'Surrey');

-- --------------------------------------------------------

--
-- Structure de la table `customer`
--

CREATE TABLE `customer` (
  `id_customer` int(10) UNSIGNED NOT NULL,
  `borndate` datetime DEFAULT NULL,
  `address_id` int(10) UNSIGNED NOT NULL,
  `loyalty_points` int(11) NOT NULL DEFAULT '0',
  `person_id` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `customer`
--

INSERT INTO `customer` (`id_customer`, `borndate`, `address_id`, `loyalty_points`, `person_id`) VALUES
(1, NULL, 1, 0, 1),
(2, '1959-11-03 00:00:00', 2, 87, 11),
(3, '1960-03-10 00:00:00', 3, 98, 12),
(4, '1980-07-30 00:00:00', 4, 97, 13),
(5, '1981-02-13 00:00:00', 5, 142, 14),
(6, '1981-08-11 00:00:00', 6, 111, 15),
(7, '1977-12-24 00:00:00', 7, 43, 16),
(8, '1980-06-23 00:00:00', 8, 38, 17),
(9, '1942-04-17 00:00:00', 9, 33, 18);

-- --------------------------------------------------------

--
-- Structure de la table `employee`
--

CREATE TABLE `employee` (
  `id_employee` int(10) UNSIGNED NOT NULL,
  `login` varchar(50) NOT NULL,
  `password` varchar(64) NOT NULL,
  `rank_id` int(10) UNSIGNED NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT '1',
  `person_id` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `employee`
--

INSERT INTO `employee` (`id_employee`, `login`, `password`, `rank_id`, `active`, `person_id`) VALUES
(1, 'Admin', 'c1c224b03cd9bc7b6a86d77f5dace40191766c485cd55dc48caf9ac873335d6f', 1, 1, 2),
(2, 'Albus', 'e4e3ca95328320ed1c9ccdf8d5ede24051aa09b341061cb667d0b409ba67887c', 1, 1, 3),
(3, 'Minerva', '838bf71261b522d42a443f0b898dce8f706076ceb02e2231f617ab793fbcec36', 2, 1, 4),
(4, 'Severus', '838bf71261b522d42a443f0b898dce8f706076ceb02e2231f617ab793fbcec36', 2, 1, 5),
(5, 'Rubeus', '838bf71261b522d42a443f0b898dce8f706076ceb02e2231f617ab793fbcec36', 2, 1, 6),
(6, 'Harry', '838bf71261b522d42a443f0b898dce8f706076ceb02e2231f617ab793fbcec36', 3, 1, 7),
(7, 'Hermionne', '838bf71261b522d42a443f0b898dce8f706076ceb02e2231f617ab793fbcec36', 3, 1, 8),
(8, 'Ron', '838bf71261b522d42a443f0b898dce8f706076ceb02e2231f617ab793fbcec36', 3, 1, 9),
(9, 'Drago', '838bf71261b522d42a443f0b898dce8f706076ceb02e2231f617ab793fbcec36', 3, 0, 10);

-- --------------------------------------------------------

--
-- Structure de la table `person`
--

CREATE TABLE `person` (
  `id_person` int(10) UNSIGNED NOT NULL,
  `lastname` varchar(50) DEFAULT '',
  `firstname` varchar(50) DEFAULT '',
  `phone` varchar(50) DEFAULT '',
  `mail` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `person`
--

INSERT INTO `person` (`id_person`, `lastname`, `firstname`, `phone`, `mail`) VALUES
(1, 'Client', 'anonyme', '', ''),
(2, 'Admin', 'Admin', '', 'Admin'),
(3, 'Dumbledore', 'Albus', '0495/22.22.22', 'Albus.Dumbledore@Poudlard.wiz'),
(4, 'McGonagall', 'Minerva', '0495/33.33.33', 'Minerva.McGonagall@Poudlard.wiz'),
(5, 'Severus', 'Rogue', '0495/44.44.44', 'Severus.Rogue@Poudlard.wiz'),
(6, 'Hagrid', 'Rubeus', '0495/55.55.55', 'Rubeus.Hagrid@Poudlard.wiz'),
(7, 'Potter', 'Harry', '0495/66.66.66', 'Harry.Potter@Poudlard.wiz'),
(8, 'Granger', 'Hermionne', '0495/77.77.77', 'Hermionne.Granger@Poudlard.wiz'),
(9, 'Weasley', 'Ron', '0495/88.88.88', 'Ron.Weasley@Poudlard.wiz'),
(10, 'Malefoy', 'Drago', '0495/99.99.99', 'Drago.Malefoy@Poudlard.wiz'),
(11, 'Black', 'Sirius', '0495/10.10.10', 'Black.Sirius@hotmail.com'),
(12, 'Lupin', 'Remus', '0495/20.20.20', 'Lupin.Remus@hotmail.com'),
(13, 'Londubat', 'Neville', '0495/30.30.30', 'Londubat.Neville@hotmail.com'),
(14, 'Lovegood', 'Luna', '0495/40.40.40', 'Lovegood.Luna@hotmail.com'),
(15, 'Weasley', 'Ginny', '0495/50.50.50', 'Weasley.Ginny@hotmail.com'),
(16, 'Delacour', 'Fleur', '0495/60.60.60', 'Delacour.Fleur@hotmail.com'),
(17, 'Dursley', 'Dudley', '0495/70.70.70', 'Dursley.Dudley@hotmail.com'),
(18, 'Rusard', 'Argus', '0495/80.80.80', 'Rusard.Argus@hotmail.com');

-- --------------------------------------------------------

--
-- Structure de la table `price_info`
--

CREATE TABLE `price_info` (
  `id_price_info` int(10) UNSIGNED NOT NULL,
  `buying_price` decimal(10,2) NOT NULL,
  `selling_price` decimal(10,2) NOT NULL,
  `tva_id` int(10) UNSIGNED NOT NULL,
  `promotion` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `price_info`
--

INSERT INTO `price_info` (`id_price_info`, `buying_price`, `selling_price`, `tva_id`, `promotion`) VALUES
(1, '13.96', '34.90', 4, 0),
(2, '13.96', '34.90', 4, 0),
(3, '13.96', '34.90', 4, 0),
(4, '13.96', '34.90', 4, 0),
(5, '13.96', '34.90', 4, 0.05),
(6, '13.96', '34.90', 4, 0),
(7, '13.96', '34.90', 4, 0),
(8, '13.96', '34.90', 4, 0),
(9, '13.96', '34.90', 4, 0),
(10, '13.96', '34.90', 4, 0.1),
(11, '13.96', '34.90', 4, 0),
(12, '13.96', '34.90', 4, 0),
(13, '13.96', '34.90', 4, 0),
(14, '13.96', '34.90', 4, 0),
(15, '13.96', '34.90', 4, 0.05),
(16, '13.96', '34.90', 4, 0),
(17, '13.96', '34.90', 4, 0),
(18, '27.98', '69.95', 4, 0),
(19, '15.18', '37.95', 4, 0),
(20, '15.18', '37.95', 4, 0.1),
(21, '15.18', '37.95', 4, 0),
(22, '15.18', '37.95', 4, 0),
(23, '15.18', '37.95', 4, 0),
(24, '15.18', '37.95', 4, 0),
(25, '15.18', '37.95', 4, 0.05),
(26, '15.18', '37.95', 4, 0),
(27, '39.98', '99.95', 4, 0),
(28, '71.98', '179.95', 4, 0),
(29, '71.98', '179.95', 4, 0),
(30, '13.98', '34.95', 4, 0.1),
(31, '13.98', '34.95', 4, 0),
(32, '13.98', '34.95', 4, 0),
(33, '13.98', '34.95', 4, 0),
(34, '5.98', '14.95', 4, 0),
(35, '5.98', '14.95', 4, 0.05),
(36, '5.98', '14.95', 4, 0),
(37, '5.98', '14.95', 4, 0),
(38, '4.78', '11.95', 4, 0),
(39, '3.98', '9.95', 4, 0),
(40, '3.98', '9.95', 4, 0.1),
(41, '13.96', '34.90', 4, 0),
(42, '13.96', '34.90', 4, 0),
(43, '13.96', '34.90', 4, 0),
(44, '13.96', '34.90', 4, 0),
(45, '13.96', '34.90', 4, 0.05),
(46, '13.96', '34.90', 4, 0),
(47, '13.96', '34.90', 4, 0),
(48, '13.96', '34.90', 4, 0),
(49, '13.96', '34.90', 4, 0),
(50, '13.96', '34.90', 4, 0.1),
(51, '35.98', '89.95', 4, 0),
(52, '35.98', '89.95', 4, 0),
(53, '55.60', '139.00', 4, 0),
(54, '19.98', '49.95', 4, 0),
(55, '27.98', '69.95', 4, 0.05),
(56, '27.98', '69.95', 4, 0),
(57, '47.60', '119.00', 4, 0),
(58, '9.98', '24.95', 4, 0),
(59, '9.98', '24.95', 4, 0),
(60, '9.98', '24.95', 4, 0.1),
(61, '15.00', '37.50', 4, 0),
(62, '15.00', '37.50', 4, 0),
(63, '17.98', '44.95', 4, 0),
(64, '17.98', '44.95', 4, 0),
(65, '17.98', '44.95', 4, 0.05),
(66, '17.98', '44.95', 4, 0),
(67, '13.96', '34.90', 4, 0),
(68, '13.96', '34.90', 4, 0),
(69, '13.96', '34.90', 4, 0),
(70, '13.96', '34.90', 4, 0.1),
(71, '13.96', '34.90', 4, 0),
(72, '13.98', '34.95', 4, 0),
(73, '13.16', '32.90', 4, 0),
(74, '13.16', '32.90', 4, 0),
(75, '13.16', '32.90', 4, 0.05),
(76, '13.16', '32.90', 4, 0),
(77, '17.98', '44.95', 4, 0),
(78, '4.78', '11.95', 4, 0),
(79, '4.78', '11.95', 4, 0.1),
(80, '4.78', '11.95', 4, 0),
(81, '4.78', '11.95', 4, 0),
(82, '39.98', '99.95', 4, 0),
(83, '23.98', '59.95', 4, 0),
(84, '57.98', '144.95', 4, 0.05),
(85, '21.98', '54.95', 4, 0),
(86, '11.98', '29.95', 4, 0),
(87, '11.98', '29.95', 4, 0),
(88, '11.98', '29.95', 4, 0),
(89, '29.98', '74.95', 4, 0.1),
(90, '23.98', '59.95', 4, 0),
(91, '71.60', '179.00', 4, 0),
(92, '47.60', '119.00', 4, 0),
(93, '33.96', '84.90', 4, 0),
(94, '19.18', '47.95', 4, 0.05),
(95, '15.18', '37.95', 4, 0),
(96, '131.60', '329.00', 4, 0),
(97, '51.60', '129.00', 4, 0),
(98, '139.60', '349.00', 4, 0),
(99, '9.96', '24.90', 4, 0.1),
(100, '25.98', '64.95', 4, 0),
(101, '10.00', '25.00', 4, 0.25),
(102, '15.00', '0.00', 4, 0),
(103, '80.00', '0.00', 4, 0),
(104, '25.00', '0.00', 4, 0),
(105, '45.00', '0.00', 4, 0),
(106, '15.00', '34.95', 4, 0),
(107, '15.00', '34.95', 4, 0),
(108, '15.00', '34.95', 4, 0),
(109, '15.00', '34.95', 4, 0),
(110, '35.00', '0.00', 4, 0);

-- --------------------------------------------------------

--
-- Structure de la table `purchase`
--

CREATE TABLE `purchase` (
  `id_purchase` int(10) UNSIGNED NOT NULL,
  `transaction_id` int(10) UNSIGNED NOT NULL,
  `article_id` int(10) UNSIGNED NOT NULL,
  `quantity` int(11) NOT NULL,
  `total` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `purchase`
--

INSERT INTO `purchase` (`id_purchase`, `transaction_id`, `article_id`, `quantity`, `total`) VALUES
(1, 6, 101, 1, '10.00'),
(2, 7, 102, 1, '15.00'),
(3, 7, 103, 1, '80.00'),
(4, 8, 104, 1, '25.00'),
(5, 8, 105, 1, '45.00'),
(6, 9, 106, 1, '15.00'),
(7, 9, 107, 1, '15.00'),
(8, 9, 108, 1, '15.00'),
(9, 9, 109, 1, '15.00'),
(10, 10, 110, 1, '35.00');

-- --------------------------------------------------------

--
-- Structure de la table `rank`
--

CREATE TABLE `rank` (
  `id_rank` int(10) UNSIGNED NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `rank`
--

INSERT INTO `rank` (`id_rank`, `name`) VALUES
(1, 'Directeur'),
(2, 'Manager'),
(3, 'Vendeur');

-- --------------------------------------------------------

--
-- Structure de la table `refund`
--

CREATE TABLE `refund` (
  `id_refund` int(10) UNSIGNED NOT NULL,
  `transaction_id` int(10) UNSIGNED NOT NULL,
  `article_id` int(10) UNSIGNED NOT NULL,
  `quantity` int(11) NOT NULL,
  `total` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `refund`
--

INSERT INTO `refund` (`id_refund`, `transaction_id`, `article_id`, `quantity`, `total`) VALUES
(1, 4, 87, 1, '29.95'),
(2, 4, 86, 1, '29.95'),
(3, 11, 87, 1, '29.95'),
(4, 11, 86, 1, '29.95'),
(6, 2, 89, 1, '67.46'),
(7, 12, 89, 1, '67.46');

-- --------------------------------------------------------

--
-- Structure de la table `sale`
--

CREATE TABLE `sale` (
  `id_sale` int(10) UNSIGNED NOT NULL,
  `transaction_id` int(10) UNSIGNED NOT NULL,
  `article_id` int(10) UNSIGNED NOT NULL,
  `quantity` int(11) NOT NULL,
  `total` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `sale`
--

INSERT INTO `sale` (`id_sale`, `transaction_id`, `article_id`, `quantity`, `total`) VALUES
(1, 1, 2, 1, '34.90'),
(2, 1, 42, 1, '34.90'),
(3, 2, 81, 2, '23.90'),
(4, 2, 89, 1, '67.46'),
(5, 3, 91, 1, '179.00'),
(6, 4, 87, 2, '59.90'),
(7, 4, 86, 2, '59.90'),
(8, 4, 88, 1, '29.95'),
(9, 5, 73, 1, '32.90'),
(10, 5, 68, 2, '69.80'),
(11, 5, 58, 1, '24.95');

-- --------------------------------------------------------

--
-- Structure de la table `sub_category`
--

CREATE TABLE `sub_category` (
  `id_sub_category` int(10) UNSIGNED NOT NULL,
  `category_id` int(10) UNSIGNED NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `sub_category`
--

INSERT INTO `sub_category` (`id_sub_category`, `category_id`, `name`) VALUES
(3, 1, 'Autres'),
(2, 1, 'Ollivander'),
(1, 1, 'Personnages'),
(4, 2, 'Creatures magiques'),
(5, 2, 'Harry Potter'),
(6, 2, 'Serre-livre'),
(8, 3, 'Coussin'),
(7, 3, 'Creatures magiques'),
(10, 4, 'Carnet'),
(11, 4, 'Ouvre-lettres'),
(12, 4, 'Porte-clés'),
(9, 4, 'Stylos et marques pages'),
(15, 5, 'Autres'),
(13, 5, 'Boucles d\'oreilles'),
(14, 5, 'Colliers'),
(16, 5, 'Lumos');

-- --------------------------------------------------------

--
-- Structure de la table `transaction`
--

CREATE TABLE `transaction` (
  `id_transaction` int(10) UNSIGNED NOT NULL,
  `employee_id` int(10) UNSIGNED NOT NULL,
  `customer_id` int(10) UNSIGNED NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `total` decimal(10,2) NOT NULL,
  `reduction` decimal(10,0) NOT NULL,
  `sum` decimal(10,2) NOT NULL,
  `tip` decimal(10,2) DEFAULT NULL,
  `type` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `transaction`
--

INSERT INTO `transaction` (`id_transaction`, `employee_id`, `customer_id`, `date`, `total`, `reduction`, `sum`, `tip`, `type`) VALUES
(1, 1, 2, '2021-09-08 00:00:00', '69.80', '0', '70.00', '0.20', 1),
(2, 1, 3, '2021-09-09 00:00:00', '81.36', '10', '81.36', '0.00', 1),
(3, 1, 4, '2021-09-11 00:00:00', '169.00', '10', '169.00', '0.00', 1),
(4, 1, 5, '2021-09-12 00:00:00', '149.75', '0', '149.75', '0.00', 1),
(5, 1, 6, '2021-09-14 00:00:00', '127.65', '0', '130.00', '2.35', 1),
(6, 1, 2, '2021-09-16 00:00:00', '-10.00', '0', '0.00', '0.00', 3),
(7, 1, 3, '2021-09-17 00:00:00', '-95.00', '0', '0.00', '0.00', 3),
(8, 1, 4, '2021-09-19 00:00:00', '-70.00', '0', '0.00', '0.00', 3),
(9, 1, 5, '2021-09-20 00:00:00', '-60.00', '0', '0.00', '0.00', 3),
(10, 1, 7, '2021-09-22 00:00:00', '-35.00', '0', '0.00', '0.00', 3),
(11, 1, 5, '2021-09-23 00:00:00', '-59.90', '0', '0.00', '0.00', 2),
(12, 1, 3, '2021-09-25 00:00:00', '-57.46', '10', '0.00', '0.00', 2);

-- --------------------------------------------------------

--
-- Structure de la table `tva`
--

CREATE TABLE `tva` (
  `id_tva` int(10) UNSIGNED NOT NULL,
  `value` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `tva`
--

INSERT INTO `tva` (`id_tva`, `value`) VALUES
(1, 0),
(2, 0.06),
(3, 0.12),
(4, 0.21);

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `address`
--
ALTER TABLE `address`
  ADD PRIMARY KEY (`id_address`),
  ADD KEY `FK_address_city` (`city_id`);

--
-- Index pour la table `article`
--
ALTER TABLE `article`
  ADD PRIMARY KEY (`id_article`),
  ADD UNIQUE KEY `Unicite_ean_code` (`ean_code`) USING BTREE,
  ADD KEY `FK_article_brand` (`brand_id`),
  ADD KEY `FK_article_price_info` (`price_info_id`),
  ADD KEY `FK_article_category` (`category_id`),
  ADD KEY `FK_article_sub_category` (`sub_category_id`);

--
-- Index pour la table `brand`
--
ALTER TABLE `brand`
  ADD PRIMARY KEY (`id_brand`),
  ADD UNIQUE KEY `Unicite_name` (`name`) USING BTREE;

--
-- Index pour la table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`id_category`),
  ADD UNIQUE KEY `Unicite_name` (`name`) USING BTREE;

--
-- Index pour la table `city`
--
ALTER TABLE `city`
  ADD PRIMARY KEY (`id_city`),
  ADD UNIQUE KEY `Unicite_name` (`name`) USING BTREE;

--
-- Index pour la table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`id_customer`),
  ADD KEY `FK_customer_address` (`address_id`),
  ADD KEY `FK_customer_person` (`person_id`);

--
-- Index pour la table `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`id_employee`),
  ADD UNIQUE KEY `Unicite_login` (`login`) USING BTREE,
  ADD KEY `FK_employee_rank` (`rank_id`),
  ADD KEY `FK_employee_person` (`person_id`);

--
-- Index pour la table `person`
--
ALTER TABLE `person`
  ADD PRIMARY KEY (`id_person`),
  ADD UNIQUE KEY `Unicite_mail` (`mail`);

--
-- Index pour la table `price_info`
--
ALTER TABLE `price_info`
  ADD PRIMARY KEY (`id_price_info`),
  ADD KEY `FK_price_info_tva` (`tva_id`);

--
-- Index pour la table `purchase`
--
ALTER TABLE `purchase`
  ADD PRIMARY KEY (`id_purchase`),
  ADD KEY `FK_purchase_article` (`article_id`),
  ADD KEY `FK_purchase_transaction` (`transaction_id`);

--
-- Index pour la table `rank`
--
ALTER TABLE `rank`
  ADD PRIMARY KEY (`id_rank`),
  ADD UNIQUE KEY `Unicite_name` (`name`) USING BTREE;

--
-- Index pour la table `refund`
--
ALTER TABLE `refund`
  ADD PRIMARY KEY (`id_refund`),
  ADD KEY `FK_refund_article` (`article_id`),
  ADD KEY `FK_refund_transaction` (`transaction_id`);

--
-- Index pour la table `sale`
--
ALTER TABLE `sale`
  ADD PRIMARY KEY (`id_sale`),
  ADD KEY `FK_sale_article` (`article_id`),
  ADD KEY `FK_sale_transaction` (`transaction_id`);

--
-- Index pour la table `sub_category`
--
ALTER TABLE `sub_category`
  ADD PRIMARY KEY (`id_sub_category`),
  ADD UNIQUE KEY `Unicite_category_name` (`category_id`,`name`) USING BTREE;

--
-- Index pour la table `transaction`
--
ALTER TABLE `transaction`
  ADD PRIMARY KEY (`id_transaction`),
  ADD KEY `FK_transaction_employee` (`employee_id`),
  ADD KEY `FK_transaction_customer` (`customer_id`);

--
-- Index pour la table `tva`
--
ALTER TABLE `tva`
  ADD PRIMARY KEY (`id_tva`),
  ADD UNIQUE KEY `Unicite_value` (`value`) USING BTREE;

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `address`
--
ALTER TABLE `address`
  MODIFY `id_address` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `article`
--
ALTER TABLE `article`
  MODIFY `id_article` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `brand`
--
ALTER TABLE `brand`
  MODIFY `id_brand` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `category`
--
ALTER TABLE `category`
  MODIFY `id_category` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `city`
--
ALTER TABLE `city`
  MODIFY `id_city` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `customer`
--
ALTER TABLE `customer`
  MODIFY `id_customer` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `employee`
--
ALTER TABLE `employee`
  MODIFY `id_employee` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `person`
--
ALTER TABLE `person`
  MODIFY `id_person` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `price_info`
--
ALTER TABLE `price_info`
  MODIFY `id_price_info` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `purchase`
--
ALTER TABLE `purchase`
  MODIFY `id_purchase` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `rank`
--
ALTER TABLE `rank`
  MODIFY `id_rank` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `refund`
--
ALTER TABLE `refund`
  MODIFY `id_refund` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `sale`
--
ALTER TABLE `sale`
  MODIFY `id_sale` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `sub_category`
--
ALTER TABLE `sub_category`
  MODIFY `id_sub_category` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `transaction`
--
ALTER TABLE `transaction`
  MODIFY `id_transaction` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `tva`
--
ALTER TABLE `tva`
  MODIFY `id_tva` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `address`
--
ALTER TABLE `address`
  ADD CONSTRAINT `FK_address_city` FOREIGN KEY (`city_id`) REFERENCES `city` (`id_city`);

--
-- Contraintes pour la table `article`
--
ALTER TABLE `article`
  ADD CONSTRAINT `FK_article_brand` FOREIGN KEY (`brand_id`) REFERENCES `brand` (`id_brand`),
  ADD CONSTRAINT `FK_article_category` FOREIGN KEY (`category_id`) REFERENCES `category` (`id_category`),
  ADD CONSTRAINT `FK_article_price_info` FOREIGN KEY (`price_info_id`) REFERENCES `price_info` (`id_price_info`),
  ADD CONSTRAINT `FK_article_sub_category` FOREIGN KEY (`sub_category_id`) REFERENCES `sub_category` (`id_sub_category`);

--
-- Contraintes pour la table `customer`
--
ALTER TABLE `customer`
  ADD CONSTRAINT `FK_customer_address` FOREIGN KEY (`address_id`) REFERENCES `address` (`id_address`),
  ADD CONSTRAINT `FK_customer_person` FOREIGN KEY (`person_id`) REFERENCES `person` (`id_person`);

--
-- Contraintes pour la table `employee`
--
ALTER TABLE `employee`
  ADD CONSTRAINT `FK_employee_person` FOREIGN KEY (`person_id`) REFERENCES `person` (`id_person`),
  ADD CONSTRAINT `FK_employee_rank` FOREIGN KEY (`rank_id`) REFERENCES `rank` (`id_rank`);

--
-- Contraintes pour la table `price_info`
--
ALTER TABLE `price_info`
  ADD CONSTRAINT `FK_price_info_tva` FOREIGN KEY (`tva_id`) REFERENCES `tva` (`id_tva`);

--
-- Contraintes pour la table `purchase`
--
ALTER TABLE `purchase`
  ADD CONSTRAINT `FK_purchase_article` FOREIGN KEY (`article_id`) REFERENCES `article` (`id_article`),
  ADD CONSTRAINT `FK_purchase_transaction` FOREIGN KEY (`transaction_id`) REFERENCES `transaction` (`id_transaction`);

--
-- Contraintes pour la table `refund`
--
ALTER TABLE `refund`
  ADD CONSTRAINT `FK_refund_article` FOREIGN KEY (`article_id`) REFERENCES `article` (`id_article`),
  ADD CONSTRAINT `FK_refund_transaction` FOREIGN KEY (`transaction_id`) REFERENCES `transaction` (`id_transaction`);

--
-- Contraintes pour la table `sale`
--
ALTER TABLE `sale`
  ADD CONSTRAINT `FK_sale_article` FOREIGN KEY (`article_id`) REFERENCES `article` (`id_article`),
  ADD CONSTRAINT `FK_sale_transaction` FOREIGN KEY (`transaction_id`) REFERENCES `transaction` (`id_transaction`);

--
-- Contraintes pour la table `sub_category`
--
ALTER TABLE `sub_category`
  ADD CONSTRAINT `FK_sub_category_category` FOREIGN KEY (`category_id`) REFERENCES `category` (`id_category`);

--
-- Contraintes pour la table `transaction`
--
ALTER TABLE `transaction`
  ADD CONSTRAINT `FK_transaction_customer` FOREIGN KEY (`customer_id`) REFERENCES `customer` (`id_customer`),
  ADD CONSTRAINT `FK_transaction_employee` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id_employee`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
