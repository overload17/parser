﻿1. Парсер страниц по шаблону как http://skay.ua/2-mobilnye-telefony/ , находиться в файле Processor.cs . По умолчанию, парсинг одной страницы происходит при загрузке, потому может показаться что долго грузиться страница. По желанию этот парсер может быть вынесен в отдельный Windows Service и работать в фоне, с определенным периодом запуска.
2. Страница просмотров товаров из БД - View.aspx.
3. Присутствует проект библиотеки DAL для работы с БД MSSQL, реализован с помощью Entity Framework.
4. В проекте Api присутствуют контроллеры для AJAX запросов.

Для запуска необходимо:
1. Создать базу данных с помощью SQL скриптов, которые есть в папке с проектом.
2. Изменить строки подключения к БД в файлах Web.config.
3. Запустить Api и проект со страницей View.aspx.
