# Парсер произвольных HTML-страниц
____

## Описание основного функционала
Приложение позволяет парсить произвольную HTML-страницу и выдавать статистику по количеству уникальных слов.
Фронтенд выполнен отдельным проектом (созданным через vue cli) на Vue.js

## Главная страница
![основная страница](https://github.com/E-A-Volobuev/PracticumTestTask/blob/master/%D0%B3%D0%BB%D0%B0%D0%B2%D0%BD%D0%B0%D1%8F%20%D1%81%D1%82%D1%80%D0%B0%D0%BD%D0%B8%D1%86%D0%B0.png)


## Парсинг
 Для парсинга HTML-страницы необходимо ввести url-адрес страницы и нажать кнопку "парсинг".
 После успешного запроса пользователь увидит выполненный запрос.
 ![основная страница](https://github.com/E-A-Volobuev/PracticumTestTask/blob/master/%D0%BF%D0%B0%D1%80%D1%81%D0%B8%D0%BD%D0%B3.png)
 
 На выбор 2 действия (2 иконки) открыть статистику , либо удалить запрос.
 
 При открытии пользователь увидит статистику повторений уникальных слов :
 ![основная страница](https://github.com/E-A-Volobuev/PracticumTestTask/blob/master/%D1%81%D1%82%D0%B0%D1%82%D0%B8%D1%81%D1%82%D0%B8%D0%BA%D0%B0%20%D0%BF%D0%BE%D0%B2%D1%82%D0%BE%D1%80%D0%B5%D0%BD%D0%B8%D0%B9.png)
 
 ## Логгирование ошибок в файл
  При возникновении ошибок в корневой папке с проектом создастся текстовый файл с информацией об ошибке (для теста в контроллер было добавлено деление на 0):
  ![основная страница](https://github.com/E-A-Volobuev/PracticumTestTask/blob/master/%D0%BB%D0%BE%D0%B3%D0%B3%D0%B5%D1%801.png)
  ![основная страница](https://github.com/E-A-Volobuev/PracticumTestTask/blob/master/%D0%BB%D0%BE%D0%B3%D0%B3%D0%B5%D1%802.png)
  ![основная страница](https://github.com/E-A-Volobuev/PracticumTestTask/blob/master/%D0%BB%D0%BE%D0%B3%D0%B3%D0%B5%D1%803.png)
  
