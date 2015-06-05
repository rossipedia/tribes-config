if(isfile("config\\MoveHudPrefs.cs"))
	exec("MoveHudPrefs.cs");

Event::Attach(EventPlayGuiCreated, MH::Init);


function MH::Init() {
	$MH::TotalHuds = 0;
	schedule("MH::InitHudLocations();", 0.5);
}

function MH::InitHudLocations() {
	%playGuiID = nameToId(playGui);
	%objs = Group::objectCount(%playGuiID);
	for(%i = 0; %i < %objs; %i++) {
		%obj = Object::getName(Group::getObject(%playGuiID, %i));
		if(%obj != false && %obj != "crosshairHud" && %obj != "clockHud" && %obj != "sensorHUD" && %obj != "healthHud" && %obj != "jetPackHud" && %obj != "compassHud" && %obj != "weaponHud" && %obj != "chatDisplayHud") {
			$MH::TotalHuds++;
			if($MHHudPref::[%obj]!="") {
        HUD::MoveTo(%obj, GetWord($MHHudPref::[%obj], 0), GetWord($MHHudPref::[%obj], 1));
			}
			$MH::HudNum[$MH::TotalHuds] = %obj;
		}
	}
}

function MH::CreateMenu() {
	%keys = "1234567890abcdefghijklmnoprstuvwxyz";
	MS::NewMenu("MoveHUDs");
	for(%i = 1; %i <= $MH::TotalHuds; %i++) {
		%key = String::GetSubStr(%keys, %a, 1);
		%a++;
		MS::AddChoice("MoveHUDs", %key, "Move "@$MH::HudNum[%i]@" now", "MH::SelectHUD("@$MH::HudNum[%i]@");");
	}
	MS::AddChoice("MoveHUDs", "q", "Quit", "MH::Quit();");
	MS::Display("MoveHUDs");
}

function MH::SelectHUD(%hud) {
	if(Control::GetVisible(%hud) == "FALSE") {
		$MH::Invis = "TRUE";
		Control::SetVisible(%hud, TRUE);
	}
	$MH::CurrentHUD = %hud;
	PushActionMap("MoveHudMap.sae");
	MH::Move();
	remoteBP(2048, "<JC><F1>Selected <F2>"@%hud@"<F1> use the arrow keys to move the hud <F2>1 (ONE)<F1> pixel at a time, Shift and Control with the arrow keys move the hud <F2>10<F1> and <F2>25<F1> pixels, respectively.");
}

function MH::Move(%direction, %shift) {
	if(%shift != "") { %offset = %shift; }
	else { %offset = 1; }
	%currentWidth = 200;
	%currentHeight = 19;
	
	%currentLoc = Control::GetPosition($MH::CurrentHUD);
	%currentX = GetWord(%currentLoc, 0);
	%currentY = GetWord(%currentLoc, 1);
	if(%direction == "up") {
		%currentY = %currentY-%offset;
    if (%currentY<0) %currentY = 0;
    HUD::MoveTo($MH::CurrentHUD, %currentX,%currentY);
    HUD::MoveTo("MH::HudOverlay", %currentX, %currentY);
	}
	else if(%direction == "down") {
		%currentY = %currentY+%offset;
    HUD::MoveTo($MH::CurrentHUD, %currentX,%currentY);
    HUD::MoveTo("MH::HudOverlay", %currentX, %currentY);
	}
	else if(%direction == "left") {
		%currentX = %currentX-%offset;
    if (%currentX < 0) %currentX = 0;
    HUD::MoveTo($MH::CurrentHUD, %currentX,%currentY);
    HUD::MoveTo("MH::HudOverlay", %currentX, %currentY);
	}
	else if(%direction == "right") {
		%currentX = %currentX+%offset;
    HUD::MoveTo($MH::CurrentHUD, %currentX,%currentY);
    HUD::MoveTo("MH::HudOverlay", %currentX, %currentY);
	}
	else {
    HUD::MoveTo($MH::CurrentHUD, %currentX, %currentY);
		if(!isObject("MH::HudOverlay")) {
			$MH::Overlay = newObject("MH::HudOverlay", FearGui::FearGuiMenu, %currentX, %currentY, %currentWidth, %currentHeight);
			$MH::Text = newObject("MH::Text", "FearGuiFormattedText", 1, 1, 0, 0);
			addToSet("MH::HudOverlay", "MH::Text");
			addToSet("playGui", "MH::HudOverlay");
		}
	}
  %currentLoc = Control::GetPosition($MH::CurrentHUD);
  %currentX = GetWord(%currentLoc, 0);
  %currentY = GetWord(%currentLoc, 1);
	Control::SetValue("MH::Text", "<JC><F1>"@$MH::CurrentHUD);
	remoteBP(2048, "<JC><F1>Selected <F2>"@$MH::CurrentHUD@"<F1> offset right at <F2>"@%currentX@"<F1> pixels and offest down at <F2>"@%currentY@"<F1> pixels.\n Press Enter when done.");
}

function MH::Quit() {
	MS::Do();
  remoteBP(2048,"");
}

function MH::Set() {
	removeFromSet("playgui", $MH::Overlay);
	deleteObject($MH::Overlay);
	%currentLoc = Control::GetPosition($MH::CurrentHUD);
	%currentX = GetWord(%currentLoc, 0);
	%currentY = GetWord(%currentLoc, 1);
	$MHHudPref::[$MH::CurrentHUD] = %currentX@" "@%currentY;
	export("$MHHudPref::*", "config\\MoveHudPrefs.cs", False);
	PopActionMap("MoveHudMap.sae");
	if($MH::Invis == "TRUE") { 
		Control::SetVisible($MH::CurrentHUD, False); 
		$MH::Invis = "";
	}
	MH::CreateMenu();
}

function MH::Keymap() {
	NewActionMap("MoveHudMap.sae");
	editActionMap("MoveHudMap.sae");
	%keys = "up down left right";
	for(%i = 0; %i < 4; %i++) {
		%key = GetWord(%keys, %i);
		bindCommand(keyboard0, make, %key, TO, "MH::Move("@%key@");");
		bindCommand(keyboard0, make, shift, %key, TO, "MH::Move("@%key@", 10);");
		bindCommand(keyboard0, make, control, %key, TO, "MH::Move("@%key@", 25);");
	}
	bindCommand(keyboard0, make, "enter", TO, "MH::Set();");
}
MH::KeyMap();
bind("control h", "MH::CreateMenu();");