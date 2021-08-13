new PlayerText:Zagruzka_TD[MAX_PLAYERS][2];
new PlayerText:Zagruzka_PTD[MAX_PLAYERS][1];
new SteepZagruzka[MAX_PLAYERS char];
new Float:SteepCricleZagruzka[MAX_PLAYERS];
new TimerZagruzka[MAX_PLAYERS];

function ecran_OnPlayerConnect( playerid )
{
	Zagruzka_TD[playerid][0] = CreatePlayerTextDraw(playerid, 0.6666, 0.6888, "LOADSUK:loadsc14"); // пусто
	PlayerTextDrawTextSize(playerid, Zagruzka_TD[playerid][0], 640.0000, 451.0000);
	PlayerTextDrawAlignment(playerid, Zagruzka_TD[playerid][0], 1);
	PlayerTextDrawColor(playerid, Zagruzka_TD[playerid][0], -1);
	PlayerTextDrawBackgroundColor(playerid, Zagruzka_TD[playerid][0], 255);
	PlayerTextDrawFont(playerid, Zagruzka_TD[playerid][0], 4);
	PlayerTextDrawSetProportional(playerid, Zagruzka_TD[playerid][0], 0);
	PlayerTextDrawSetShadow(playerid, Zagruzka_TD[playerid][0], 0);

	Zagruzka_TD[playerid][1] =  CreatePlayerTextDraw(playerid, 71.6666, 408.0370, "LD_SPAC:white"); // пусто
	PlayerTextDrawTextSize(playerid, Zagruzka_TD[playerid][1], 0.9092, 19.0000);
	PlayerTextDrawAlignment(playerid, Zagruzka_TD[playerid][1], 1);
	PlayerTextDrawColor(playerid, Zagruzka_TD[playerid][1], -1);
	PlayerTextDrawBackgroundColor(playerid, Zagruzka_TD[playerid][1], 255);
	PlayerTextDrawFont(playerid, Zagruzka_TD[playerid][1], 4);
	PlayerTextDrawSetProportional(playerid, Zagruzka_TD[playerid][1], 0);
	
	Zagruzka_PTD[playerid][0] = CreatePlayerTextDraw(playerid, 71.6666, 408.0369, "LD_SPAC:white"); // пусто
	PlayerTextDrawTextSize(playerid, Zagruzka_PTD[playerid][0], 0.9092, 19.0000);
	PlayerTextDrawAlignment(playerid, Zagruzka_PTD[playerid][0], 1);
	PlayerTextDrawColor(playerid, Zagruzka_PTD[playerid][0], 1211993087);
	PlayerTextDrawBackgroundColor(playerid, Zagruzka_PTD[playerid][0], 255);
	PlayerTextDrawFont(playerid, Zagruzka_PTD[playerid][0], 4);
	PlayerTextDrawSetProportional(playerid, Zagruzka_PTD[playerid][0], 0);
	PlayerTextDrawSetShadow(playerid, Zagruzka_PTD[playerid][0], 0);
	PlayerTextDrawHide(playerid, Zagruzka_TD[playerid][0]);
	PlayerTextDrawHide(playerid, Zagruzka_TD[playerid][1]);
	PlayerTextDrawHide(playerid, Zagruzka_PTD[playerid][0]);
	return 1;
}

stock StartZagruzka(playerid) {
	TogglePlayerControllable(playerid, 0);
    PlayerTextDrawShow(playerid, Zagruzka_TD[playerid][0]);
    PlayerTextDrawShow(playerid, Zagruzka_TD[playerid][1]);
    PlayerTextDrawShow(playerid, Zagruzka_PTD[playerid][0]);
    TimerZagruzka[playerid] = SetTimerEx(!"@Zagruzka", GetPlayerPing(playerid)/45, true, !"i", playerid);
}

@Zagruzka(playerid);
@Zagruzka(playerid) {
	if(SteepZagruzka{playerid} < 100) {
		SteepZagruzka{playerid}++;
		SteepCricleZagruzka[playerid] += 1.4;
		PlayerTextDrawTextSize(playerid, Zagruzka_PTD[playerid][0], SteepCricleZagruzka[playerid], 19.0000);
		PlayerTextDrawShow(playerid, Zagruzka_PTD[playerid][0]);
	} else {
		SteepZagruzka{playerid} = 0;
		SteepCricleZagruzka[playerid] = 0.0;
		PlayerTextDrawHide(playerid, Zagruzka_TD[playerid][0]);
		PlayerTextDrawHide(playerid, Zagruzka_TD[playerid][1]);
		PlayerTextDrawHide(playerid, Zagruzka_PTD[playerid][0]);
		PlayerTextDrawTextSize(playerid, Zagruzka_PTD[playerid][0], 0.0, 90.0);
		TogglePlayerControllable(playerid, 1);
		KillTimer(TimerZagruzka[playerid]);
	}
	return 1;
}
