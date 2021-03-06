﻿// Класс описывает методы для связи с сервером
var Server = function ()
{
	this.APIURL = '/API';

	// Функция выполняет запрос и вызывает callBackFunction в случае успеха с параметром callBackParam и callBackFailFunction в случае ошибки
	this.PerformPostRequest = function (url, obj, callBackFunction, callBackFailFunction, callBackParam) {

		var myCallBackFunction = callBackFunction;
		var myCallBackFailFunction = callBackFailFunction || this.DefaultAlertFunction;
		var myCallBackParam = callBackParam;

		var url = this.APIURL + '/' + url;

		var request = $.ajax({
			url: url,
			data: JSON.stringify(obj),
			type: "GET",
			dataType: 'json',
			contentType: 'application/json; charset=utf-8',
			context: window
		});

		request.done(function (answer)
		{
			if (!answer) {
				myCallBackFailFunction('Неизвестная ошибка выполнения запроса', myCallBackParam);
				return;
			}
			if (answer.Error)
			{
				myCallBackFailFunction(answer.Error, myCallBackParam);
				return;
			}
			myCallBackFunction(answer, myCallBackParam);
		});

		request.fail(function (jqxhr, textStatus, error)
		{
			myCallBackFailFunction(error, myCallBackParam);
		});
	}


  // Получить страницу моделей с сервера
	this.GetLeagues = function (CountryCode, GoodsResultCallBack)
	{
		var myGoodsResultCallBack = GoodsResultCallBack;
		function GoodResult(data)
		{
			myGoodsResultCallBack(data);
    }

		this.PerformPostRequest('GetLeagues?CountryCode=' + CountryCode, {}, GoodResult);

  }

	// Получить одну модель с сервера
	this.GetTeams = function (LeagueId, GoodsResultCallBack) {
		var myGoodsResultCallBack = GoodsResultCallBack;
		function GoodResult(data) {
			myGoodsResultCallBack(data);
		}

		this.PerformPostRequest('GetTeams?LeagueId=' + LeagueId, {}, GoodResult);

	}

	// Вывод сообщения об ошибке по умолчанию
	this.DefaultAlertFunction = function (txt)
	{
		alert(txt);
	}


}



