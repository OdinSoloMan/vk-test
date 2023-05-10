# vk-test
<div>
  <div>API приложение на ASP.NET Core</div>
  <div>Требования бизнес-логики и ограничения</div>
  <br>
  <div>
    <ul>
      <li>Формат запроса/ответа должен быть <strong>JSON.</strong></li>
      <li>Методы API должны быть асинхронными.</li>
      <li>В качестве СУБД необходимо использовать PostgreSQL.</li>
      <li>В качестве ORM необходимо использовать <strong>EntityFrameworkCore.</strong></li>
      <li>В качестве моделей данных должны использоваться следующие сущности:
        <ul>
          <li><strong>user </strong>(id, login, password, created_date, user_group_id, user_state_id)</li>
          <li><strong>user_group </strong>(id, code, description)&nbsp;Возможные значения для code (Admin, User)</li>
          <li><strong>user_state </strong>(id, code, description)&nbsp;Возможные значения для code (Active, Blocked).
          </li>
        </ul>
      </li>
    </ul>
  </div>
  <br>
  <div>
    - Приложение должно позволять добавлять/удалять/получать пользователей. Получить можно как одного, так и всех
    пользователей (добавление/удаление только по одному). При получении пользователей должна возвращаться полная
    информация о них (с user_group и user_state).<br>
    - Система должна не позволять иметь более одного пользователя с user_group.code = “Admin”.<br>
    - После успешной регистрации нового пользователя, ему должен быть выставлен статус "Active". Добавление нового
    пользователя должно занимать 5 сек. За это время при попытке добавления пользователя с таким же login должна
    возвращаться ошибка.<br>
    - Удаление пользователя должно осуществляться не путём физического удаления из таблицы, а путём выставления статуса
    "Blocked" у пользователя.<br>
    - Допускается добавлять вспомогательные данные в существующие таблицы.
  </div>
  <br>
  <div>ОПЦИОНАЛЬНО</div>
  <br>
  <div>
    <ul>
      <li>В качестве способа авторизации следует использовать Basic-авторизацию.</li>
      <li>Реализовать пагинацию для получения нескольких пользователей.</li>
      <li>Написать unit-тесты с помощью xUnit.</li>
    </ul>
  </div>
</div>
