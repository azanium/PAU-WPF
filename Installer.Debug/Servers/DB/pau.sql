/*
SQLyog Ultimate v8.71 
MySQL - 5.1.41 : Database - beacukai
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`beacukai` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `beacukai`;

/*Table structure for table `nationality_attention` */

DROP TABLE IF EXISTS `nationality_attention`;

CREATE TABLE `nationality_attention` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `Nationality` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `nationality_attention` */

insert  into `nationality_attention`(`ID`,`Nationality`) values (1,'USAF'),(2,'ID');

/*Table structure for table `pau_dpo` */

DROP TABLE IF EXISTS `pau_dpo`;

CREATE TABLE `pau_dpo` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(500) DEFAULT NULL,
  `first_name` varchar(100) DEFAULT NULL,
  `last_name` varchar(100) DEFAULT NULL,
  `passport` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

/*Data for the table `pau_dpo` */

/*Table structure for table `pau_passenger` */

DROP TABLE IF EXISTS `pau_passenger`;

CREATE TABLE `pau_passenger` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(500) DEFAULT NULL,
  `first_name` varchar(100) DEFAULT NULL,
  `last_name` varchar(100) DEFAULT NULL,
  `passport` varchar(500) DEFAULT NULL,
  `pnr` varchar(100) DEFAULT NULL,
  `fare_class` varchar(100) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `seq_no` varchar(50) DEFAULT NULL,
  `seat_no` varchar(100) DEFAULT NULL,
  `cdst` varchar(50) DEFAULT NULL,
  `flight_date` date DEFAULT NULL,
  `flight_no` varchar(50) DEFAULT NULL,
  `gender` varchar(50) DEFAULT NULL,
  `birth_date` date DEFAULT NULL,
  `nationality` varchar(50) DEFAULT NULL,
  `notes` varchar(500) DEFAULT NULL,
  `picture` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=1025 DEFAULT CHARSET=latin1;

/*Data for the table `pau_passenger` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
