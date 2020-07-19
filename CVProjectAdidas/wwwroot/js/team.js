// Класс определяет модель одной Команды
var Team = function () {
  this.name = undefined;
  this.code = undefined;
  this.logo = undefined;
  this.country = undefined;
  this.venue_name = undefined;
  this.venue_surface = undefined;
  this.venue_address = undefined;
  this.venue_city = undefined;
  this.is_national = undefined;
  this.team_id = undefined;
  this.venue_capacity = undefined;
  this.founded = undefined;

  // Получим данные с сервера
  this.LoadFromServer = function (data) {
    this.name = data.name;
    this.code = data.code;
    this.logo = data.logo;
    this.country = data.country;
    this.venue_name = data.venue_name;
    this.venue_surface = data.venue_surface;
    this.venue_address = data.venue_address;
    this.venue_city = data.venue_city;
    this.is_national = data.is_national;
    this.team_id = data.team_id;
    this.venue_capacity = data.venue_capacity;
    this.founded = data.founded;
  }

  // Загрузим данные в форму
  this.ViewToCard = function () {
    $("#Team_Card_name").val(this.name);
    
    $("#Team_Card_country").val(this.country);

    $("#Team_Card_venue_name").val(this.venue_name);
    $("#Team_Card_venue_surface").val(this.venue_surface);
    $("#Team_Card_venue_address").val(this.venue_address);
    $("#Team_Card_venue_city").val(this.venue_city);

    $("#Team_Card_venue_capacity").val(this.venue_capacity);
    $("#Team_Card_founded").val(this.founded);

    $('#Team_Card_logo').attr('src', this.logo || "img/noimage.svg")


  }

  // Вернуть HTML строки в таблице моделей
  this.GetHTML = function () {
    var tr = '<tr ondblclick="OpenTeam(' + this.team_id + ')">';
    tr += ' <td>';
    tr += '   <img src="' + (this.logo || "img/noimage.svg")+ '" >';
    tr += ' </td>';
    tr += ' <td>' + this.name + '</td>';
    tr += ' <td onclick="OpenTeam(' + this.team_id + ')"><span class="TeamLink">Подробнее...</span> </td>';
    tr += '</tr>';

    return tr;
  }



}