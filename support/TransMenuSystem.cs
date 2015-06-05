
$MS::TextHeight = 14;
$MS::CenterPrintOffset = 40;


function MS::NewMenu(%menuName,%x,%y) {  
  %menuName = String::Replace(%menuName, " ", "_");
  DeleteVariables("$MS::"@%menuName@"*");
  $MS::[%menuName] = %menuName;
  MS::SetLoc(%menuName,%x,%y);
}


function MS::AddChoice(%menuName, %key, %title, %function) {
	%menuName = String::Replace(%menuName, " ", "_");
	if(!$MS::[%menuname, actionMap]) { 
		NewActionMap(%MenuName@"Map.sae");
		$MS::[%menuname, actionMap] = "TRUE";
		bindCommand(keyboard0, make, "escape", TO, "MS::Do();");
	}
	EditActionMap(%MenuName@"Map.sae");
	%tmp = $MS::[%menuName, Item]++;
	$MS::FunctionNum++;
	$MS::[%menuName, %tmp] = "<F2>"@%key@". <F1>"@%title;
	$MS::Function[$MS::FunctionNum] = %function;
	bindCommand(keyboard0, make, %key, TO, "MS::Do("@$MS::FunctionNum@");");
	bindCommand(keyboard0, break, %key, TO, "MS::Break();");
  $MS::[%menuName,ht]+=$MS::TextHeight;
}

function MS::Break() { }

function MS::AddMenu(%parentMenu, %key, %menuName) {
	%menuName = String::Replace(%menuName, " ", "_");
	%parentMenu = String::Replace(%parentMenu, " ", "_");
	if(!$MS::[%parentMenu, actionMap]) { 
		NewActionMap(%parentMenu@"Map.sae");
		$MS::[%parentMenu, actionMap] = "TRUE";
		bindCommand(keyboard0, make, "escape", TO, "MS::Do();");
	}
	EditActionMap(%parentMenu@"Map.sae");
	%tmp = $MS::[%parentMenu, Item]++;
	$MS::[%parentMenu, %tmp] = "<F2>"@%key@". <F1>"@%menuName;
	bindCommand(keyboard0, make, %key, TO, "MS::Display("@%menuName@");");
	bindCommand(keyboard0, break, %key, TO, "MS::Break();");
  $MS::[%parentMenu,ht]+=$MS::TextHeight;
  %loc = $MS::[%parentMenu,loc];
  if (%loc == "")
    MS::SetLoc(%menuName,$MS::[%parentMenu,x],$MS::[%parentMenu,y]);
  else
    MS::SetLoc(%menuName,$MS::[%parentMenu,loc]);
}

function MS::Display(%menuName) {
  if ($MenuObjCreated!="TRUE")
  {
    MS::Create();
  }
	%menuName = String::Replace(%menuName, " ", "_");
	if($MS::[%menuName] != "" && %menuName != $MS::CurrentMenu) {
		%text = "\t<JL><F0>"@String::UCase(%menuName)@"\n";
		for(%i = 1; %i <= $MS::[%menuName, Item]; %i++) {
			%text = %text@"\t\t"@$MS::[%menuName, %i]@"\n";
		}
    %pos = MS::GetPos(%menuName);
    Control::SetPosition(MS::MenuObj,getWord(%pos,0),getWord(%pos,1));
    Control::SetValue(MS::MenuObj,%text);
    
		PushActionMap(%MenuName@"Map.sae");
		if($MS::CurrentMenu != "") { PopActionMap($MS::CurrentMenu@"Map.sae"); }
		$MS::CurrentMenu = %menuName;
	}
	else {
    if ($MS::[%menuName]=="")
      echo("Invalid menu call.  Menu does not exist.");
	}
}

function MS::AddBreak(%menuName,%text)
{
	%menuName = String::Replace(%menuName, " ", "_");
	if(!$MS::[%menuname, actionMap]) { 
		NewActionMap(%MenuName@"Map.sae");
		$MS::[%menuname, actionMap] = "TRUE";
		bindCommand(keyboard0, make, "escape", TO, "MS::Do();");
	}
	%tmp = $MS::[%menuName, Item]++;
	$MS::FunctionNum++;
	if (%text!="")
		$MS::[%menuName, %tmp] = %text;
	else
		$MS::[%menuName, %tmp] = "<f0>=====================================";
}

function MS::Do(%functionNum) {
  //remoteBP(2048, "");
  Control::SetValue(MS::MenuObj,"");
	PopActionMap($MS::CurrentMenu@"Map.sae");
	$MS::CurrentMenu = "";
	eval($MS::Function[%functionNum]);
}


function MS::Create()
{
  if (isObject($MENUOBJECT))
    MS::Clear(); 
  $MENUOBJECT = newObject(MS::MenuObj,FearGuiFormattedText,0,0,0,0);
  addToSet(PlayGui,$MENUOBJECT);
  $MenuObjCreated="TRUE";
}

function MS::Clear()
{
  removeFromSet(PlayGui,$MENUOBJECT);
  if (isObject($MENUOBJECT))
    deleteObject($MENUOBJECT);
}
Event::Attach(eventExit,MS::Clear);

function MS::GetPos(%menuName)
{
  %loc = $MS::[%menuName,loc];
  if (%loc == "")
    return $MS::[%menuName,x]@" "@$MS::[%menuName,y];
  else
  {
    %scrX = getScreenSize(x);
    %scrY = getScreenSize(y);
    if (%loc=="CP")
      %y = (%scrY/2)-($MS::[%menuName,ht]/2);
    else if (%loc=="BP")
      %y = (%scrY-20)-($MS::[%menuName,ht]);
    else if (%loc=="TP")
      %y = (%scrY+20);
    return $MS::CenterPrintOffset@" "@%y;
  }
}

function MS::SetLoc(%menuName,%x,%y)
{
  if (%x=="" && %y=="")
    %x = "CP";
  if (IsNum(%x,"whole"))
    $MS::[%menuName,x] = %x;
  else if (%x=="CP" || %x=="BP" || %x=="TP")
    $MS::[%menuName,loc] = %x;
  if (IsNum(%y,"whole"))
    $MS::[%menuName,y] = %y;
}
