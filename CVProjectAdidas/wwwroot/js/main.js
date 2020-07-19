var me = {};

$(function () {
  me.server = new Server();
  me.teamData = null;
  

  InitEvents();


});



function InitEvents()
{



  $("#Teams_Table_Country_Select").change(function () {
    function ViewLeague(data) {
      if (!data.LeagueList)
      {
        alert('Для данной страны нет данных по турнирам')
        return;
      }

      var html = '<option></option>';

      for (var i in data.LeagueList)
      {
        var l = data.LeagueList[i];
        if (!l.league_id) continue;
        html += '<option value="' + l.league_id + '">' + l.name + ' (' + l.season + ')</option>';
      }

      $("#Teams_Table_League_Select").html(html);
    }

    var CountryCode = $("#Teams_Table_Country_Select").val();
    $("#Teams_Table_League_Select").html('');
    if (!CountryCode) return;
    me.server.GetLeagues(CountryCode, ViewLeague);  
  });



  $("#Teams_Table_League_Select").change(function () {
    function ViewTeams(data) {
      if (!data.TeamList) {
        alert('Для данной страны нет данных по турнирам')
        return;
      }

      console.log(data);

      me.teamData = [];
      var html = '';

      for (var i in data.TeamList) {
        var t = data.TeamList[i];
        if (!t.team_id) continue;
        var oneTeam = new Team();
        oneTeam.LoadFromServer(t);
        me.teamData[t.team_id] = oneTeam;
        html += oneTeam.GetHTML();
      }

      $("#Teams_Table_Body").html(html);
    }

    var LeagueId = $("#Teams_Table_League_Select").val();
    $("#Teams_Table_Body").html('');
    if (!LeagueId) return;
    me.server.GetTeams(LeagueId, ViewTeams);
  });
  
};



function OpenTeam(TeamId)
{
  if (!me.teamData[TeamId])
  {
    alert('Не найдена команда');
    return;
  }

  me.teamData[TeamId].ViewToCard();
  $("#Team_Card_Modal").modal();
  //console.log(me.teamData[TeamId]);

}