-- phpMyAdmin SQL Dump
-- version 4.9.5deb2
-- https://www.phpmyadmin.net/
--
-- Хост: localhost:3306
-- Время создания: Фев 29 2024 г., 18:01
-- Версия сервера: 8.0.30-0ubuntu0.20.04.2
-- Версия PHP: 7.4.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `КнижныйМагазин`
--

DELIMITER $$
--
-- Процедуры
--
CREATE DEFINER=`**********`@`%` PROCEDURE `ВсеЗаказыДляПользователя` (IN `idcust` INT)  NO SQL
BEGIN 
SELECT * from Заказ WHERE Заказ.Номер=idcust;
END$$

--
-- Функции
--
CREATE DEFINER=`**********`@`%` FUNCTION `УдалитьПользователя` (`iduser` INT) RETURNS TINYINT NO SQL
BEGIN 
DELETE from Заказ WHERE Заказ.НомерПользователя=iduser;
	DELETE from Пользователь WHERE Пользователь.Номер=iduser;
    RETURN TRUE;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Структура таблицы `Заказ`
--

CREATE TABLE `Заказ` (
  `Номер` int NOT NULL,
  `НомерПользователя` int NOT NULL,
  `Цена` float NOT NULL DEFAULT '0',
  `ДатаЗаказа` datetime NOT NULL,
  `ДатаИзмененияСтатусаЗаказа` datetime NOT NULL,
  `Статус` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Заказ`
--

INSERT INTO `Заказ` (`Номер`, `НомерПользователя`, `Цена`, `ДатаЗаказа`, `ДатаИзмененияСтатусаЗаказа`, `Статус`) VALUES
(1, 4, 264.8, '2023-03-14 11:40:54', '2023-06-13 14:51:09', 'Завершён'),
(2, 9, 155.59, '2023-03-14 13:14:18', '2023-03-14 13:14:18', 'Оформлен'),
(3, 8, 155.59, '2023-03-14 13:15:39', '2023-03-20 09:39:19', 'Отменён'),
(4, 9, 93.1, '2023-03-14 13:22:09', '2023-03-20 09:35:56', 'Завершён'),
(5, 4, 120.83, '2023-03-23 05:31:00', '2023-06-27 00:53:00', 'Завершён'),
(6, 11, 20, '2023-03-23 08:36:40', '2023-06-27 00:53:14', 'В пути'),
(7, 4, 20, '2023-03-23 09:04:42', '2023-03-23 09:04:42', 'Оформлен'),
(8, 13, 35, '2023-03-23 09:07:25', '2023-06-27 00:53:18', 'Принят'),
(9, 15, 20, '2023-03-23 09:11:00', '2023-06-27 00:53:24', 'Отменён'),
(10, 14, 75, '2023-03-23 09:14:54', '2023-06-27 00:53:28', 'Завершён'),
(11, 10, 20, '2023-03-23 09:16:19', '2023-06-27 00:53:32', 'Доставлен'),
(12, 7, 20, '2023-03-23 09:20:50', '2023-06-27 00:53:35', 'Завершён'),
(13, 7, 20, '2023-03-23 09:28:50', '2023-06-27 00:53:38', 'Принят'),
(14, 9, 20, '2023-03-23 09:32:10', '2023-06-27 00:53:42', 'Доставлен'),
(15, 6, 20, '2023-03-23 10:06:53', '2023-06-27 00:53:46', 'Принят'),
(16, 4, 20, '2023-03-23 10:13:59', '2023-06-27 00:53:49', 'В пути'),
(17, 4, 20, '2023-03-23 10:23:55', '2023-06-27 00:53:53', 'В пути'),
(18, 6, 60, '2023-03-23 18:23:55', '2023-06-25 14:33:25', 'Доставлен'),
(19, 7, 70, '2023-04-28 11:50:48', '2023-04-28 11:52:16', 'Доставлен'),
(20, 15, 70, '2023-05-15 12:17:19', '2023-05-30 16:20:03', 'Принят'),
(21, 4, 84, '2023-05-30 16:24:59', '2023-05-30 16:24:59', 'Оформлен'),
(22, 4, 175, '2023-05-31 12:51:43', '2023-05-31 12:51:43', 'Оформлен'),
(23, 8, 20.83, '2023-06-05 07:14:59', '2023-06-27 00:54:27', 'В пути'),
(24, 10, 159.8, '2023-06-05 07:15:55', '2023-06-27 00:54:31', 'Завершён'),
(25, 9, 149.9, '2023-06-05 07:16:31', '2023-06-27 00:54:35', 'Завершён'),
(26, 12, 20, '2023-06-05 13:34:05', '2023-06-27 00:54:38', 'Доставлен'),
(27, 13, 132, '2023-06-05 13:34:33', '2023-06-27 00:54:42', 'Доставлен'),
(28, 14, 79.9, '2023-06-05 15:52:35', '2023-06-27 00:54:45', 'Доставлен'),
(29, 4, 139.65, '2023-06-13 14:54:16', '2023-06-27 00:54:48', 'Завершён'),
(30, 4, 27.26, '2023-06-25 14:41:55', '2023-06-27 00:54:52', 'В пути'),
(31, 4, 44.76, '2023-06-26 14:47:30', '2023-06-27 00:55:01', 'Доставлен'),
(32, 4, 56.22, '2023-06-27 00:58:32', '2023-06-27 00:58:32', 'Оформлен'),
(33, 4, 23.42, '2023-06-27 09:16:24', '2023-06-27 09:16:24', 'Оформлен'),
(34, 4, 41.66, '2023-06-27 11:13:51', '2023-06-27 11:13:51', 'Оформлен');

-- --------------------------------------------------------

--
-- Структура таблицы `ЗаказКнига`
--

CREATE TABLE `ЗаказКнига` (
  `НомерЗаказа` int NOT NULL,
  `НомерКниги` int NOT NULL,
  `Количество` int NOT NULL,
  `Номер` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `ЗаказКнига`
--

INSERT INTO `ЗаказКнига` (`НомерЗаказа`, `НомерКниги`, `Количество`, `Номер`) VALUES
(1, 1, 2, 1),
(1, 2, 3, 2),
(2, 5, 2, 3),
(3, 5, 2, 4),
(4, 5, 2, 5),
(5, 3, 1, 7),
(6, 4, 1, 8),
(7, 4, 1, 11),
(8, 2, 1, 12),
(9, 4, 1, 13),
(10, 2, 1, 14),
(10, 4, 2, 15),
(11, 4, 1, 16),
(12, 4, 1, 17),
(13, 4, 1, 18),
(14, 4, 1, 19),
(15, 4, 1, 20),
(16, 4, 1, 22),
(17, 4, 1, 23),
(18, 4, 3, 24),
(19, 2, 2, 25),
(20, 2, 2, 26),
(21, 4, 3, 27),
(21, 7, 2, 28),
(22, 2, 5, 29),
(23, 3, 1, 30),
(24, 1, 2, 31),
(25, 1, 1, 32),
(25, 2, 2, 33),
(26, 4, 1, 37),
(27, 9, 3, 38),
(28, 1, 1, 48),
(29, 5, 3, 49),
(30, 13, 2, 50),
(30, 14, 1, 51),
(31, 18, 2, 52),
(31, 17, 1, 53),
(32, 9, 2, 54),
(32, 10, 1, 55),
(33, 11, 2, 56),
(34, 3, 2, 57);

-- --------------------------------------------------------

--
-- Структура таблицы `Книга`
--

CREATE TABLE `Книга` (
  `Номер` int NOT NULL,
  `Название` text NOT NULL,
  `Цена` float NOT NULL,
  `Описание` text NOT NULL,
  `Жанр` text NOT NULL,
  `Автор` text NOT NULL,
  `Фото` text NOT NULL,
  `Количество` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Книга`
--

INSERT INTO `Книга` (`Номер`, `Название`, `Цена`, `Описание`, `Жанр`, `Автор`, `Фото`, `Количество`) VALUES
(1, 'Маленький Принц', 79.9, '«Ма́ленький принц» (фр. Le Petit Prince) — аллегорическая повесть-сказка, наиболее известное произведение Антуана де Сент-Экзюпери.\r\n\r\nСказка рассказывает о Маленьком принце, который посещает различные планеты в космосе, включая Землю. Книга обращается к темам одиночества, дружбы, любви и утраты. Несмотря на стиль детской книги, её герой рассуждает о жизни и человеческой природе[2].\r\n\r\nВпервые опубликована 6 апреля 1943 года в Нью-Йорке. Было продано более 140 миллионов экземпляров по всему миру, что поставило её в ряд самых продаваемых книг.\r\n\r\nРисунки в книге выполнены самим автором и не менее знамениты, чем сама книга. Это не только иллюстрации, но и органическая часть произведения: сами герои сказки ссылаются на рисунки и спорят о них. Уникальные иллюстрации в «Маленьком принце» разрушают языковые барьеры, становятся частью универсального визуального лексикона, понятного каждому.\r\n\r\n«Ведь все взрослые сначала были детьми, только мало кто из них об этом помнит», — Антуан де Сент-Экзюпери, из посвящения к книге.', 'Повесть', 'Антуан де Сент-Экзюпери', 'http://185.87.50.136/bookshopimages/prince.jpg', 58),
(2, 'Дежавю', 35, '\"Дежавю\" – максимально откровенная и провокационная автобиография самого известного российского рэп – артиста KIZARU. Лайфстайл Олега Нечипоренко уже давно ассоциируется с жизнью, о которой тайком мечтают сотни тысяч – стремительная карьера, побег из России, миллионы подписчиков в соцсетях.\r\n\r\nЭта книга честный рассказ об изнанке популярности, предательстве друзей и жизненных уроках. И пусть биография автора неоднозначна, его предприимчивость и талант привели музыканта на вершину российского хип-хопа.\r\n\r\nКнига дополнена редкими иллюстрациями из личного архива KIZARU.', 'Автобиография', 'Олег Нечипоренко', 'http://185.87.50.136/bookshopimages/dejavu.jpg', 93),
(3, 'Карты Таро', 20.83, 'Таро Уэйта – одна из самых знаменитых колод Таро в мире! Ее создатель – известный исследователь Каббалы и мистических учений Артур Эдвард Уэйт.\r\n\r\nОбразы карт, созданных Уэйтом, настолько просты и понятны, что могут дать правдивый ответ даже новичку в гадании. И в то же время Таро Уэйта – это серьезная эзотерическая практика, которая поможет вам разобраться в себе и своих чувствах, отделить истинные желания от ложных, найти свой путь в жизни, увидеть решение сложной ситуации и понять намерения других людей.', 'Рассказ', 'Артур Эдвард Уэйт', 'http://185.87.50.136/bookshopimages/taro.jpg', 89),
(4, 'Цветы для Элджернона', 20, 'Сорок лет назад это считалось фантастикой. Сорок лет назад это читалось как фантастика. Исследующая и расширяющая границы жанра, жадно впитывающая всевозможные новейшие веяния, примеряющая общечеловеческое лицо, отважно игнорирующая каинову печать \"жанрового гетто\". Сейчас это воспринимается как одно из самых человечных произведений новейшего времени, как роман пронзительной психологической силы, как филигранное развитие темы любви и ответственности. Не зря вышедшую уже в 1990-е книгу воспоминаний Киз назвал \"Элджернон, Чарли и я\".', 'Рассказ', 'Дэниел Киз', 'http://185.87.50.136/bookshopimages/flowers.jpg', 290),
(5, 'Человек бензопила', 46.55, 'Дэндзи – обычный молодой человек, который промышляет охотой на демонов, чтобы расплатиться с долгами покойного отца. Если он хоть раз опоздает с выплатой, его ждет смерть. Дэндзи живет в крошечной лачуге со своим демоническим псом-бензопилой по имени Почита и едва сводит концы с концами. Все о чем он мечтает – ходить на свидания с девушкой и есть хлеб с джемом. Но у судьбы на него другие планы...\r\n\r\nВ один прекрасный день юноша обретает сверхъестественные силы, позволяющие ему превращать свое тело в живое оружие. Дэндзи вырывается из нищеты и под рев дребезжащих бензопил и крики умирающих демонов начинает прорезать себе дорогу в новую жизнь...', 'Манга', 'Тацуки Фудзимото', 'http://185.87.50.136/bookshopimages/benzopila.jpg', 237),
(7, 'Шестёрка воронов', 44.04, 'Каз Бреккер никогда не снимает черных перчаток. Но, если не хочешь стать ужином для акул, не спрашивай его, почему. Никому не известно, где его семья, откуда он пришел и почему остался в Кеттердаме.\r\n\r\nЗато он знает обо всех и все. Бреккер – правая рука главаря одной из самых влиятельных банд в городе. Казино, бордели, нелегальная торговля – его стихия. А еще шантаж, грабеж и, если понадобится, хладнокровное убийство. Но все это мелочи по сравнению с новым заказом. На кону – баснословные деньги и... секрет, который может уничтожить одни народы и возвеличить другие. Какие именно – теперь зависит от Каза и его команды. Шестерых \"воронов\", которым нечего терять кроме надежды. Это дело объединит их.', 'Рассказ', 'Ли Бардуго', 'http://185.87.50.136/bookshopimages/sixvorons.jpg', 208),
(8, 'Испытание', 29.57, 'Последняя битва не прошла бесследно ни для кого. Флинт зол на весь мир, Джексон превращается в нечто с трудом узнаваемое, а Хадсон воздвиг вокруг себя стены, которые я вряд ли смогу сломать.\r\n\r\nГрядет война, и мы к ней не готовы. Только наличие армии может подарить хоть какую-то надежду на победу. Но прежде всего мне нужно разобраться со своим прошлым.\r\n\r\nЯ должна найти ответы, которые помогут определить, кто из нас настоящий монстр.\r\n\r\nНайти истинное чудовище в мире, наполненном кровожадными вампирами, бессмертными горгульями и непрекращающейся враждой богов.', 'Рассказ', 'Трейси Вульф', 'http://185.87.50.136/bookshopimages/challenge.jpg', 32),
(9, 'Как Саша стал здоровым. Практикум по психосоматике', 20.02, 'Как соединить вместе 5 компонентов здоровья и найти свой алгоритм исцеления от болезней и проблем?\r\n\r\nНовая книга провокативного психолога Ирины Семизоровой посвящена одной из самых сакральных тайн мира. Эта тайна – здоровье нашего тела в его взаимосвязи с душой и духом.\r\n\r\nКнига будет интересна каждому, кто готов к увлекательному самоисследованию и хочет знать как расшифровать сигналы, которые посылает нам наше тело.\r\n\r\n', 'Рассказ', 'Ирина Семизорова', 'http://185.87.50.136/bookshopimages/howsasha.jpg', 45),
(10, 'Война и мир (в 2-х томах)', 16.18, '\"Война и мир\" Л. Н. Толстого – книга на все времена. Кажется, что она существовала всегда, настолько знакомым кажется текст, едва мы открываем первые страницы романа, настолько памятны многие его эпизоды: охота и святки, первый бал Наташи Ростовой, лунная ночь в Отрадном, князь Андрей в сражении при Аустерлице... Сцены \"мирной\", семейной жизни сменяются картинами, имеющими значение для хода всей мировой истории, но для Толстого они равноценны, связаны в едином потоке времени. Каждый эпизод важен не только для развития сюжета, но и как одно из бесчисленных проявлений жизни, которая насыщена в каждом своём моменте и которую учит любить Толстой.', 'Эпопея', 'Лев Толстой', 'http://185.87.50.136/bookshopimages/warandpeace.jpg', 731),
(11, 'Властелин колец. Две твердыни', 11.71, 'Отряд хранителей Кольца распадается, а война приходит на земли Рохана – государства вольных Всадников, союзников Гондора. Арагорн, Гимли и Леголас вместе с младшими хоббитами помогают рохирримам в жестокой битве против сил тёмного мага Сарумана, дорога же Фродо и Сэма лежит к Изгарным горам и далее, в мрачную крепость Кирит-Унгол, где открывается потайной ход в Тёмную страну.', 'Эпопея', 'Джон Рональд Руэл Толкин', 'http://185.87.50.136/bookshopimages/kolca.jpg', 9),
(12, 'Одиссея', 8.21, 'Эпопея \"Одиссея\", созданная гением легендарного эпического поэта Древней Греции Гомера, – одно из величайших произведений мировой литературы. Опасные приключения хитроумного и многострадального Одиссея (ему, страстно стремящемуся домой в Итаку, предстоит вступить в единоборство с Посейдоном, побывать в стране жестоких людоедов и на острове волшебницы Кирки, а затем отправиться к порогу царства мертвых), нежные чувства героев и страсти, бушующие на страницах поэмы, легенды и тайны, которыми овеяно прошлое, наконец, сам язык, которым написана поэма Гомера, необычайно образный и яркий, – все это вновь и вновь притягивает к себе внимание все новых читателей, вдохновляет поэтов и художников.\r\n\r\n', 'Эпопея', 'Гомер', 'http://185.87.50.136/bookshopimages/odiccea.jpg', 2),
(13, 'Эпос о Гильгамеше', 7.5, '\"Эпос о Гильгамеше\" – старейшее художественное произведение, известное человечеству. Когда в XVIII веке до нашей эры шумерские поэты начали заполнять первые глиняные таблички, повествуя о подвигах правителя города Урук, не существовало еще ни одного европейского государства, а на Древнем Востоке едва брезжила заря цивилизации. Тем интереснее современным читателям главные герои эпоса – полубог Гильгамеш и его друг-соперник Энкиду. Их чувства и поступки нам близки и понятны. Приобщение к высокой культуре, скорбь после смерти друга, желание бессмертия – эти главные темы древнего эпоса можно назвать вечными.', 'Эпос', 'Джордж Смит', 'http://185.87.50.136/bookshopimages/epos.jpg', 2),
(14, 'Калевала', 12.26, 'Кропотливо собиравшийся много лет известным финским фольклористом Элиасом Лённротом, этот эпос вызвал в международном сообществе ученых-фольклористов до сих пор не прекратившуюся войну между сторонниками и противниками Лённрота, упрекавшими собирателя в произвольном составлении текстов, их \"причесывании\" и прочих профессиональных грехах. Однако академические споры уже не могли остановить победоносного шествия \"Калевалы\" по миру – на ее основе писали пьесы, оперы и балеты, ее иллюстрировали знаменитые художники и переводили на ведущие языки мира, по ней снимали фильмы, мультфильмы и сериалы, а в Финляндии и Карелии в ее честь учреждены национальные праздники.', 'Эпос', 'Э. Лённрот', 'http://185.87.50.136/bookshopimages/Calevala.jpg', 10),
(15, 'Великий Гэтсби', 8.06, '\"Бурные\" двадцатые годы прошлого столетия... Время шикарных вечеринок, \"сухого закона\" и \"легких\" денег... Эти \"новые американцы\" уверены, что расцвет будет вечным, что достигнув вершин власти и богатства, они обретут и личное счастье... Таким был и Джей Гэтсби, ставший жертвой бессмысленной погони за пленительной мечтой об истинной и вечной любви, которой не суждено было сбыться... Перед вами – самый знаменитый роман Ф. С. Фицджеральда в новом переводе!', 'Роман', 'Фрэнсис Скотт Фицджеральд', 'http://185.87.50.136/bookshopimages/getsby.jpg', 24),
(16, 'Анна Каренина', 8.21, '\"Анна Каренина\" – лучший роман о женщине, написанный в XIX веке. По словам Ф. М. Достоевского, \"Анна Каренина\" поразила современников \"не только вседневностью содержания, но и огромной психологической разработкой души человеческой, страшной глубиной и силой\". Уже к началу 1900-х годов роман Толстого был переведен на многие языки мира, а в настоящее время входит в золотой фонд мировой литературы.', 'Роман', 'Лев Толстой', 'http://185.87.50.136/bookshopimages/anna.jpg', 44),
(17, 'Тарас Бульба', 7.16, 'Повесть Н. В. Гоголя \"Тарас Бульба\" уже современники сопоставляли с гомеровским эпосом. Как и автор \"Илиады\", Гоголь отталкивается от исторических событий и придает им эпические масштабы. Мужество, непреклонная воля, недюжинная сила и любовь к отечеству – черты истинных героев в \"Тарасе Бульбе\". Может быть, поэтому яркие образы, созданные Гоголем в этой повести, оказываются столь привлекательными для читателей и вызывают желание режиссеров вновь и вновь воплощать фантазию писателя на экране.\r\n\r\n', 'Повесть', 'Николай Гоголь', 'http://185.87.50.136/bookshopimages/bulba.jpg', 32),
(18, 'Ветер в ивах', 18.8, 'Если ваш ребёнок уже умеет читать, самое время познакомить его с шедеврами детской литературы. А чтобы чтение стало для него любимым и необходимым занятием, книги этой серии специально адаптированы с учётом его возрастных особенностей и словарного запаса. Они помогут привить ему хороший литературный вкус – тогда, повзрослев, он непременно захочет прочитать классический текст и по достоинству оценит его.\r\n\r\nСказочная повесть английского писателя Кеннета Грэма про незабываемые приключения Крыса, Крота, Барсука и Жаба на протяжении века увлекает и детей и взрослых.\r\n\r\nИздание украшают великолепные иллюстрации австралийского художника Роберта Ингпена.', 'Повесть', 'Кеннет Грэм', 'http://185.87.50.136/bookshopimages/vind.jpg', 2),
(19, 'Гранатовый браслет', 7.07, 'В этот сборник вошли повести и рассказы разных периодов. И венчают его три самых пронзительных произведения Куприна о любви, которая \"сильнее, чем смерть, и стрелы ее – стрелы огненные\".\r\n\r\nЗавораживающая, трагическая фантазия \"Суламифь\", рассказывающая о прекрасной и чистой любви царя Соломона и девушки Суламифь. \"Гранатовый браслет\" – печальная повесть о безвозвратно потерянной любви, развеянной по ветру мелодией сонаты Бетховена. И, наконец, \"Олеся\" – история любви барского сына и живущей в лесу девушки-колдуньи Олеси, которой не суждено было завершиться счастливым концом – то ли темные силы, то ли предрассудки, то ли просто судьба разлучили влюбленных навсегда.', 'Новелла', 'Александр Куприн', 'http://185.87.50.136/bookshopimages/braslet.jpg', 122),
(20, 'Отцы и дети', 7.72, '\"Отцы и дети\" – знаменитая новелла Тургенева, ставший чуть ли не самым значительным произведением в истории о взаимоотношениях поколений. Споры главного героя Евгения Базарова, называющего себя нигилистом и отрицающего расхожие представления о жизни, искусстве, морали, природе человека, и его антагониста Павла Кирсанова, аристократа до мозга костей, составляют главную проблематику романа.\r\n\r\nНо сюжетная канва строится скорее на внутреннем конфликте самого Базарова. Только что отрицавший все и вся герой, не веривший в любовь, смеявшийся над своим приятелем, способным на нежные чувства, вдруг сам страстно и пылко влюбляется.\r\n\r\nИ все вдруг переворачивается вверх дном – с любовью Базаров совладать не в силах, она оказывается сильнее самых твердых убеждений.', 'Новелла', 'Иван Тургенев', 'http://185.87.50.136/bookshopimages/dadssons.jpg', 71),
(21, 'Скетчи по воскресеньям. Как несерьезные эксперименты вырастают в крутые идеи и меняют нашу жизнь навсегда', 15, 'Сборник забавных иллюстраций и необычных мыслей о творчестве от художника, автора и лауреата многих наград Кристофа Ниманна. В стиле своей колонки Abstract Sunday (\"Абстрактное воскресенье\") в New York Times Ниманн рассказывает историю своей карьеры и ведет захватывающую хронику современной жизни в эскизах, дневниках и заметках в популярных газетах. Он весело и умно повествует о творческом процессе, карьере в искусстве и попытках преодоления внешних и внутренних препятствий, с которыми сталкиваются творческие люди.\r\n\r\nКнига содержит почти 350 оригинальных иллюстраций и станет неиссякаемым источником вдохновения и мотивации.\r\n\r\nКнига для людей творческих профессий. Для всех, кого вдохновили книги Остина Клеона.', 'Скетч', 'Кристоф Ниманн', 'http://185.87.50.136/bookshopimages/weekendsketchi.jpg', 1),
(22, 'От идеи до скетча. Сторителлинг. Советы и лайфхаки 50 профессиональных художников жанра', 51.64, 'Сторителлинг – одна из ключевых составляющих любого рисунка. Правильно выстроенный нарратив в иллюстрации может поведать целую историю, развернувшуюся на маленьком клочке бумаги. В книге 50 талантливых художников со всего мира расскажут, как создать запоминающуюся и цепляющую взгляд работу, как выстраивать повествование в своей работе и почему об этом должен знать каждый иллюстратор. Они поделятся историями о своем творческом пути, разберут свои скетчи и концепт-арты и покажут, что такое хорошая иллюстрация.\r\n\r\n', 'Скетч', '3dtotal', 'http://185.87.50.136/bookshopimages/fromidia.jpg', 3),
(23, 'Горе от ума', 8.22, 'Комедия А. С. Грибоедова \"Горе от ума\" была написана уже более полутора столетий назад, но живописная картина нравов, галерея живых типов и вечно острая ирония по-прежнему волнуют и увлекают читателей, учат их достоинству и благородству. Подлинная вершина русской драматургии начала XIX века, пьеса \"Горе от ума\", известна нам со школьных лет. Но о других произведениях Грибоедов сегодня знают мало. Между тем он вовсе не был автором одного произведения. В настоящем издании представлены сочинения Грибоедова разных лет, позволяющие составить более полное представление о его творческом наследии.', 'Пьеса', 'Александр Грибоедов', 'http://185.87.50.136/bookshopimages/goreotuma.jpg', 129),
(24, 'Вишневый сад', 15.73, 'В эту книгу вошли классические пьесы Чехова, вот уже столетие не сходящие со сцены, многократно экранизированные самыми блистательными режиссерами мира и по сей день не утратившие своей гениальной, вечной актуальности.', 'Пьеса', 'Антон Чехов', 'http://185.87.50.136/bookshopimages/vishneviysad.jpg', 111),
(25, 'Пляска смерти', 23.6, 'Одна из наиболее известных нехудожественных работ Стивена Кинга, в которой он выступает не в роли писателя, а в роли критика, вдумчиво, тонко и с юмором анализирующего жанр ужасов.\r\n\r\nНаписанная на основе цикла лекций Кинга \"Особенности литературы о сверхъестественном\", эта книга расскажет много нового о \"культуре ужасов\" в кино, на радио и, конечно же, в литературе. Особенно вдохновенно Стивен Кинг повествует о жанре ужасов в кинематографе - и даже прилагает к книге список своих любимых фильмов.\r\n\r\nИтак, почему же нам так нравятся \"страшные\" истории? Какие произведения классиков американской литературы ХХ века повлияли на развитие жанра? И, наконец, какие фильмы до дрожи пугают самого Мастера?', 'Очерк', 'Стивен Кинг', 'http://185.87.50.136/bookshopimages/plyaskasmerti.jpg', 351),
(26, 'Своя комната', 20.04, '500 фунтов в год и своя комната – это главное, что нужно женщине для творчества, по мнению Вирджинии Вулф.\r\n\r\n\"Своя комната\" – знаменитое эссе, основанное на лекциях, которые Вулф прочитала в Ньюнхэм-колледже и Гёртон-колледже – двух женских колледжах Кембриджского университета – в октябре 1928 года. В нем она обращается ко всем женщинам, занимающимся литературой, и вспоминает своих великих предшественниц – Джейн Остен, Шарлотту Бронте, Джордж Элиот, – вынужденных писать в общей гостиной, прятать рукописи подальше от посторонних глаз и все время сталкиваться с мнением, что писательский труд – недостойное занятие для женщины.', 'Эссе', 'Вирджиния Вулф', 'http://185.87.50.136/bookshopimages/myroom.jpg', 123),
(27, 'Колокол Нагасаки', 22.31, 'Когда 9 августа 1945 года над Нагасаки взорвалась атомная бомба \"Толстяк\", доктор Такаси Нагаи работал в университетской больнице неподалеку от эпицентра. Едва выбравшись из полуразрушенного здания и несмотря на серьезные травмы, вместе с другими врачами он принялся оказывать помощь выжившим. Отчет о первых страшных днях после атомной бомбардировки лег в основу книги \"Колокол Нагасаки\".\r\n\r\nДоктор Нагаи умер от лейкемии спустя два года после публикации. Его эссе стало и уникальным свидетельством очевидца катастрофы, и размышлением ученого о природе атомной бомбы, и гуманистическим призывом: подобное никогда больше не должно повториться.', 'Эссе', 'Такаси Нагаи', 'http://185.87.50.136/bookshopimages/colocol.jpg', 32),
(28, 'Бедная Лиза', 5.91, 'Н. М. Карамзин открыл новую эпоху русской литературы, став основоположником сентиментализма – направления, пришедшего на смену классицизму и привлекшего внимание к миру человеческих чувств. В сборник вошли самые известные повести писателя: \"Наталья, боярская дочь\", \"Марфа-Посадница\", \"Сиерра-Морена\", \"Остров Борнгольм\", \"Юлия\" и, конечно же, \"Бедная Лиза\" – хрестоматийное произведение, пленяющее своим лиризмом и элегической тональностью. Именно в этой повести начали разрабатываться темы, привлекшие затем внимание Пушкина, Гоголя и Достоевского.', 'Эссе', 'Николай Карамзин', 'http://185.87.50.136/bookshopimages/bednaya.jpg', 10),
(29, 'Зороастрийская книга мертвых. Доктрина загробной жизни', 11.5, 'Впервые на русском языке предлагается весьма ценное фактографическое сочинение известного индийского ученого. В центре внимания - зороастрийская эсхатология (доктрина обновления мира после конца света) и сотериология (доктрина вечной жизни). По древним текстам автор скрупулезно собрал и восстановил цепь загробных судебно-юридических процедур, показав их ближневосточные корни. Книга была задумана как первая часть трилогии о загробной жизни в представлении древних парсов.\r\n\r\n', 'Видения', 'Джал Паври', 'http://185.87.50.136/bookshopimages/doctorina.jpg', 66),
(30, 'Баллада о змеях и певчих птицах', 19.54, 'Действие книги разворачивается за 64 года до событий первого романа \"Голодные игры\". Главный герой – будущий президент-диктатор Кориолан Сноу. История о том, как \"Голодные игры\" из прямолинейной бойни превратились в сверкающее и гламурное шоу.', 'Баллада', 'Сьюзен Коллинз', 'http://185.87.50.136/bookshopimages/ozmeyah.jpg', 29);

-- --------------------------------------------------------

--
-- Структура таблицы `Отзыв`
--

CREATE TABLE `Отзыв` (
  `Номер` int NOT NULL,
  `НомерПользователя` int NOT NULL,
  `НомерКниги` int NOT NULL,
  `Отзыв` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Отзыв`
--

INSERT INTO `Отзыв` (`Номер`, `НомерПользователя`, `НомерКниги`, `Отзыв`) VALUES
(4, 14, 16, 'Эта книга — настоящий литературный алмаз. Каждая страница наполнена яркими образами, глубокими мыслями и проникновенными эмоциями. Я не мог оторваться от чтения и влюбился в каждого персонажа'),
(6, 12, 3, 'Читая эту книгу, я погрузился в параллельную реальность, полную загадок и приключений. Стройность сюжета, тонкое чувство юмора и неожиданные повороты делают ее невероятно увлекательной. Рекомендую всем любителям фантастики'),
(7, 10, 4, 'Эта книга изменила мою жизнь. Она научила меня смотреть на мир по-другому, переосмысливать свои ценности и искать смысл в каждом мгновении. Прекрасное произведение, которое оставляет глубокий след в сердце и развивает ум'),
(8, 15, 13, 'Я ожидал от этой книги гораздо большего. Сюжет размытый, персонажи непроработанные, а стиль написания вызывает сонливость. Чтение стало скучным и утомительным. Не рекомендую'),
(9, 13, 25, 'Мне было сложно сосредоточиться на этой книге из-за ее нелогичного сюжета и несвязанных событий. Персонажи кажутся плоскими и нереалистичными, а диалоги неубедительными. Не мой выбор'),
(10, 10, 8, 'Эта книга пытается быть глубокой и философской, но вместо этого она становится многословной и занудной. Сюжет затянутый, а персонажи неинтересные. Я бы не рекомендовал тратить на нее время'),
(11, 11, 1, 'Это произведение истинное литературное воплощение волшебства. Автор умело создаёт прекрасные образы и захватывающие сюжеты, которые пленяют с первых страниц и не отпускают до последней строки'),
(12, 10, 2, 'Книга, которая оставляет после себя глубокий след в душе. Прекрасный язык, эмоциональная глубина и уникальная атмосфера, которая переносит читателя в совершенно иной мир.'),
(13, 10, 9, 'К сожалению, эта книга не оправдала моих ожиданий. Плоские персонажи, незаинтересовавший сюжет и запутанный язык делают чтение трудным и скучным. Не рекомендую'),
(14, 7, 2, 'Я пытался заинтересоваться этой книгой, но она не смогла меня захватить. Сюжет размыт, персонажи невыразительны, а стиль написания не вызывает никаких эмоций. Я ожидал большего'),
(15, 13, 25, 'К сожалению, эта книга не оправдала моих ожиданий. Плоские персонажи, незаинтересовавший сюжет и запутанный язык делают чтение трудным и скучным. Не рекомендую'),
(16, 12, 17, 'К сожалению, эта книга оказалась полным разочарованием. Сюжет пустой и предсказуемый, а стиль написания скучный и неоригинальный. Я не смог найти в ней ничего ценного или интересного'),
(17, 4, 16, 'Книга, которая полностью захватывает воображение! Удивительно проработанный мир, интригующий сюжет и яркие персонажи — все это делает ее настоящим литературным шедевром. Я просто влюбился в эту книгу и рекомендую ее всем!'),
(18, 8, 20, 'Эта книга — настоящая находка для любителей детективов! Загадочные интриги, неожиданные повороты событий и раскрытие загадок до самого конца создают захватывающую атмосферу. Читать ее — настоящее наслаждение'),
(19, 13, 12, 'Очаровательная история, наполненная мудростью и нежностью. Она учит нас ценить каждый момент жизни и понимать важность межличностных отношений. Прекрасно написанная книга, которая оставляет после себя глубокие впечатления'),
(20, 4, 4, 'Я честно говоря не понял смысла этой книги. Сюжет запутанный, персонажи непроницаемые, а стиль написания неестественный. Я ожидал большего и остался разочарован.'),
(21, 10, 10, 'Эта книга кажется бессвязной и бесполезной. Нет никакой структуры и никакой истории, которую можно было бы уловить. Чтение ее было настоящим испытанием'),
(22, 15, 7, 'Я был разочарован этой книгой. Сюжет предсказуемый, персонажи поверхностные, а стиль написания скучный. Я не смог найти в ней ничего, что было бы стоящим внимания'),
(23, 4, 17, 'Эта книга — настоящее литературное воплощение воображения! Захватывающий сюжет, яркие описания и невероятные приключения погружают вас в мир полный магии и возможностей. Я просто не мог оторваться от ее страниц'),
(24, 6, 18, 'Книга, которая перевернула мое представление о любви и судьбе. Умелое сочетание эмоций, сильных персонажей и интригующего сюжета создает неповторимую атмосферу, которая цепляет с первых строк. Однозначно рекомендую!'),
(25, 7, 19, 'Я был поражен мастерством автора в создании мрачной и загадочной атмосферы. Книга не только захватывает своими поворотами событий, но и заставляет задуматься о глубинных философских вопросах. Великолепное произведение!'),
(26, 7, 20, 'Книга, которую я читал, не оправдала моих ожиданий. Сюжет был запутанным и непонятным, персонажи плоскими, а язык написания скучным и неинтересным. Я не смог полностью погрузиться в историю и разочаровался'),
(27, 10, 21, 'Эта книга — настоящая жемчужина литературы! Она переносит читателя в магический мир, полный приключений и неожиданных открытий. Прекрасно проработанный сюжет и яркие персонажи заставляют сердце биться быстрее. Настоятельно рекомендую'),
(29, 4, 23, 'Я просто не мог оторваться от этой книги! Она захватывает с первых строк и уносит в мир приключений и фантазии. Сильные персонажи, захватывающие сюжетные повороты и потрясающая атмосфера делают ее настоящим литературным шедевром.'),
(30, 14, 24, 'Честно говоря, эта книга мне не понравилась. Сюжет размытый, персонажи невыразительные, а стиль написания вызывает скуку. Я ожидал большего и остался разочарован'),
(31, 6, 25, 'Книга, которую я не мог дочитать до конца. Сюжет плоский и предсказуемый, персонажи неинтересные, а стиль написания скучный. Чтение стало настоящей пыткой.'),
(32, 12, 26, 'Я не понимаю, почему эта книга так высоко оценивается. Сюжет банальный, персонажи стереотипные, а стиль написания неоригинальный. Я бы не рекомендовал тратить время на ее прочтение'),
(34, 7, 28, 'Это произведение истинное литературное воплощение волшебства. Автор умело создаёт прекрасные образы и захватывающие сюжеты, которые пленяют с первых страниц и не отпускают до последней строки'),
(36, 7, 30, 'Книга, которая дотрагивается до самых глубинных чувств и эмоций. Она умело показывает, что любовь и надежда могут возникнуть даже в самых безнадежных ситуациях. Прекрасно написанная и проникновенная история, которая оставляет след в сердце');

-- --------------------------------------------------------

--
-- Структура таблицы `Поддержка`
--

CREATE TABLE `Поддержка` (
  `Номер` int NOT NULL,
  `НомерПользователя` int NOT NULL,
  `Вопрос` text NOT NULL,
  `Ответ` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Поддержка`
--

INSERT INTO `Поддержка` (`Номер`, `НомерПользователя`, `Вопрос`, `Ответ`) VALUES
(2, 4, 'Как заказать книгу?', '-'),
(3, 9, 'Можно ли заказать книгу, чтобы посмотреть её в живую?', '-'),
(4, 10, 'Нужно ли все время иметь доступ в интернет для работы с приложением?', 'Да'),
(5, 10, 'Как происходит оплата?', 'Наличными при получении'),
(6, 7, 'Как оставить отзыв к книге?', 'Начать смотреть подробную информацию о книге -> Написать отзыв'),
(7, 6, 'Как сменить пароль', '-'),
(8, 8, 'Можно ли зайти с другого устройства', '-'),
(9, 8, 'Как посмотреть книгу', ''),
(10, 4, 'Как почитать отзывы', '');

-- --------------------------------------------------------

--
-- Структура таблицы `Пользователь`
--

CREATE TABLE `Пользователь` (
  `Номер` int NOT NULL,
  `Имя` text NOT NULL,
  `Фамилия` text NOT NULL,
  `Роль` text NOT NULL,
  `Пароль` text NOT NULL,
  `Почта` text NOT NULL,
  `ДатаРегистрации` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Пользователь`
--

INSERT INTO `Пользователь` (`Номер`, `Имя`, `Фамилия`, `Роль`, `Пароль`, `Почта`, `ДатаРегистрации`) VALUES
(4, 'Антон', 'Антонов', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'egorchuzhavko@gmail.com', '2023-02-01 15:11:18'),
(5, 'admin', 'admin', 'Администратор', 'tZxnvxlqR1gZHkL3ZnDOug==', 'admin', '2023-05-01 15:11:26'),
(6, 'Владимир', 'Пантенол', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'qwerty@gmail.com', '2023-05-23 18:05:32'),
(7, 'Владислав', 'Вишневский', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'fjfkfk@gmail.com', '2023-05-30 11:35:35'),
(8, 'Егор', 'Чужавко', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'qwertyqwerty@gmail.com', '2023-06-05 07:07:31'),
(9, 'Владимир', 'Сова', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'qwerty1@gmail.com', '2023-05-23 18:05:32'),
(10, 'Георгий', 'Якубович', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'fjfkfk2@gmail.com', '2023-05-30 11:35:35'),
(11, 'Алексей', 'Чужавко', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'qwertyqwerty1@gmail.com', '2023-06-05 07:07:31'),
(12, 'Андрей', 'Рудинский', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'qwertyqwerty2@gmail.com', '2023-06-05 07:07:31'),
(13, 'Яна', 'Зеленкова', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'qwerty3@gmail.com', '2023-05-23 18:05:32'),
(14, 'Карина', 'Лужинская', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'fjfkfk3@gmail.com', '2023-05-30 11:35:35'),
(15, 'Павел', 'Орлов', 'Покупатель', 'AQR+nw6Tv1hQXt4MMOoLDA==', 'qwertyqwerty5@gmail.com', '2023-06-05 07:07:31');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Заказ`
--
ALTER TABLE `Заказ`
  ADD PRIMARY KEY (`Номер`),
  ADD KEY `НомерПользователя` (`НомерПользователя`);

--
-- Индексы таблицы `ЗаказКнига`
--
ALTER TABLE `ЗаказКнига`
  ADD PRIMARY KEY (`Номер`),
  ADD KEY `НомерЗаказа` (`НомерЗаказа`),
  ADD KEY `НомерКниги` (`НомерКниги`);

--
-- Индексы таблицы `Книга`
--
ALTER TABLE `Книга`
  ADD PRIMARY KEY (`Номер`);

--
-- Индексы таблицы `Отзыв`
--
ALTER TABLE `Отзыв`
  ADD PRIMARY KEY (`Номер`),
  ADD KEY `НомерКниги` (`НомерКниги`),
  ADD KEY `НомерПользователя` (`НомерПользователя`);

--
-- Индексы таблицы `Поддержка`
--
ALTER TABLE `Поддержка`
  ADD PRIMARY KEY (`Номер`),
  ADD KEY `НомерПользователя` (`НомерПользователя`);

--
-- Индексы таблицы `Пользователь`
--
ALTER TABLE `Пользователь`
  ADD PRIMARY KEY (`Номер`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Заказ`
--
ALTER TABLE `Заказ`
  MODIFY `Номер` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;

--
-- AUTO_INCREMENT для таблицы `ЗаказКнига`
--
ALTER TABLE `ЗаказКнига`
  MODIFY `Номер` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=58;

--
-- AUTO_INCREMENT для таблицы `Книга`
--
ALTER TABLE `Книга`
  MODIFY `Номер` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT для таблицы `Отзыв`
--
ALTER TABLE `Отзыв`
  MODIFY `Номер` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT для таблицы `Поддержка`
--
ALTER TABLE `Поддержка`
  MODIFY `Номер` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT для таблицы `Пользователь`
--
ALTER TABLE `Пользователь`
  MODIFY `Номер` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `Заказ`
--
ALTER TABLE `Заказ`
  ADD CONSTRAINT `Заказ_ibfk_1` FOREIGN KEY (`НомерПользователя`) REFERENCES `Пользователь` (`Номер`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Ограничения внешнего ключа таблицы `ЗаказКнига`
--
ALTER TABLE `ЗаказКнига`
  ADD CONSTRAINT `ЗаказКнига_ibfk_1` FOREIGN KEY (`НомерЗаказа`) REFERENCES `Заказ` (`Номер`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `ЗаказКнига_ibfk_2` FOREIGN KEY (`НомерКниги`) REFERENCES `Книга` (`Номер`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Ограничения внешнего ключа таблицы `Отзыв`
--
ALTER TABLE `Отзыв`
  ADD CONSTRAINT `Отзыв_ibfk_1` FOREIGN KEY (`НомерКниги`) REFERENCES `Книга` (`Номер`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Отзыв_ibfk_2` FOREIGN KEY (`НомерПользователя`) REFERENCES `Пользователь` (`Номер`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Ограничения внешнего ключа таблицы `Поддержка`
--
ALTER TABLE `Поддержка`
  ADD CONSTRAINT `Поддержка_ibfk_1` FOREIGN KEY (`НомерПользователя`) REFERENCES `Пользователь` (`Номер`) ON DELETE RESTRICT ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
