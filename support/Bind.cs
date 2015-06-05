

function bind(%key, %onpress, %onrelease, %id, %map,%device)
{
  
  if (%key == "")
  {
    echo("No Key specified for command: \""@escapestring(%onpress)@"\".");
    return;
  }
  if (%onPress=="")
  {
    echo("Please specify a command to bind.");
    return;
  }
  if(%device=="")
  {
    if (String::FindSubStr(%key,"button")!=-1||String::FindSubStr(%key,"zaxis")!=-1)
      %device = "mouse";
    else
      %device = "keyboard";
  }
  if (%map=="")
    %map = "PlayMap.sae";
  if (%onrelease=="")
    %onrelease = "null();";

  if (%id!="")
  {
    if ($Bind::[%id,key]=="")
    {
      $Bind::Count++;
      $Bind::[%id,idx] = $Bind::Count;
      $Bind::List[%idx] = %id;
    }
    $Bind::[%id, make] = %onpress;
    $Bind::[%id, break] = %onrelease;
    $Bind::[%id,device] = %device;
    $Bind::[%id,map] = %map;
    $Bind::[%id,key] = %key;
  }

  for (%i=0;(%word=getWord(%key,%i))!=-1;%i++)
  {
    if (getWord(%key,%i+1)!=-1)
      %newkey = %newkey@", "@%word;
    else
      %newkey = %newkey@", \""@%word@"\"";
  }
  %key = %newkey;
  editActionMap(%map);
  eval("bindCommand("@%device@"0, make"@%key@", TO, \""@escapestring(%onpress)@"\");");
  eval("bindCommand("@%device@"0, break"@%key@", TO, \""@escapestring(%onrelease)@"\");");
}
function null() {}


function GetUserBind(%command,%description,%cmddone,%tag)
{
	$Bind::IP = "TRUE";
	pushActionMap("BindMap.sae");
	remoteCP(2048, "<jc>Press the new keybind for: <f2>"@%description);
	$Bind::BindCommand = %command;
	$Bind::Description = %description;
	$Bind::ExecCmd= %cmddone;
  $Bind::Tag = %tag;
}

function Bind::press(%keybind)
{
	popActionMap("BindMap.sae");
  $Bind::IP = "";
	remoteCP(2048, "<jc><f2>"@$Bind::Description@"<f0> set to:<f2> "@%keybind,3);
  if ($Bind::[$Bind::Tag,key]!="")
    bind($Bind::[$Bind::Tag,key],"null();");
	bind(%keybind,$Bind::BindCommand,"",$Bind::Tag);
	if($Bind::ExecCmd != "")
		eval($Bind::ExecCmd@"(\""@%keybind@"\");");
}

function Bind::Releasemod(%modifier)
{
	if($Bind::IP)
		Bind::press(%modifier);
}

function Bind::Cancelbind()
{
	popActionMap("BindMap.sae");
  $Bind::IP = "";
  remoteCP(2048,"");
}


function Bind::Setup()
{
  
  NewActionMap("BindMap.sae");
  
  bindCommand(keyboard0, make, "escape", TO, "Bind::cancelbind();");
		

	bindCommand(keyboard0, break, "lalt", TO, "Bind::Releasemod(\"lalt\");");
	bindCommand(keyboard0, break, "lcontrol", TO, "Bind::Releasemod(\"lcontrol\");");
	bindCommand(keyboard0, break, "lshift", TO, "Bind::Releasemod(\"lshift\");");
	bindCommand(keyboard0, break, "ralt", TO, "Bind::Releasemod(\"ralt\");");
	bindCommand(keyboard0, break, "rcontrol", TO, "Bind::Releasemod(\"rcontrol\");");
	bindCommand(keyboard0, break, "rshift", TO, "Bind::Releasemod(\"rshift\");");


	bindCommand(keyboard0, make, "tab", TO, "Bind::Press(\"tab\");");
	bindCommand(keyboard0, make, "capslock", TO, "Bind::Press(\"capslock\");");
	bindCommand(keyboard0, make, "backspace", TO, "Bind::Press(\"backspace\");");
	bindCommand(keyboard0, make, "enter", TO, "Bind::Press(\"enter\");");
	bindCommand(keyboard0, make, "insert", TO, "Bind::Press(\"insert\");");
	bindCommand(keyboard0, make, "delete", TO, "Bind::Press(\"delete\");");
	bindCommand(keyboard0, make, "home", TO, "Bind::Press(\"home\");");
	bindCommand(keyboard0, make, "end", TO, "Bind::Press(\"end\");");
	bindCommand(keyboard0, make, "prior", TO, "Bind::Press(\"prior\");");
	bindCommand(keyboard0, make, "next", TO, "Bind::Press(\"next\");");
	bindCommand(keyboard0, make, "space", TO, "Bind::Press(\"space\");");

	bindCommand(keyboard0, make, "numpad0", TO, "Bind::Press(\"numpad0\");");
	bindCommand(keyboard0, make, "numpad1", TO, "Bind::Press(\"numpad1\");");
	bindCommand(keyboard0, make, "numpad2", TO, "Bind::Press(\"numpad2\");");
	bindCommand(keyboard0, make, "numpad3", TO, "Bind::Press(\"numpad3\");");
	bindCommand(keyboard0, make, "numpad4", TO, "Bind::Press(\"numpad4\");");
	bindCommand(keyboard0, make, "numpad5", TO, "Bind::Press(\"numpad5\");");
	bindCommand(keyboard0, make, "numpad6", TO, "Bind::Press(\"numpad6\");");
	bindCommand(keyboard0, make, "numpad7", TO, "Bind::Press(\"numpad7\");");
	bindCommand(keyboard0, make, "numpad8", TO, "Bind::Press(\"numpad8\");");
	bindCommand(keyboard0, make, "numpad9", TO, "Bind::Press(\"numpad9\");");
	
	bindCommand(keyboard0, make, "numpad/", TO, "Bind::Press(\"numpad/\");");
	bindCommand(keyboard0, make, "numpad*", TO, "Bind::Press(\"numpad*\");");
	bindCommand(keyboard0, make, "numpad-", TO, "Bind::Press(\"numpad-\");");
	bindCommand(keyboard0, make, "numpad+", TO, "Bind::Press(\"numpad+\");");
	bindCommand(keyboard0, make, "Decimal", TO, "Bind::Press(\"decimal\");");
	bindCommand(keyboard0, make, "numpadenter", TO, "Bind::Press(\"numpadenter\");");

	bindCommand(keyboard0, make, "up", TO, "Bind::Press(\"up\");");
	bindCommand(keyboard0, make, "down", TO, "Bind::Press(\"down\");");
	bindCommand(keyboard0, make, "left", TO, "Bind::Press(\"left\");");
	bindCommand(keyboard0, make, "right", TO, "Bind::Press(\"right\");");

  bindCommand(keyboard0, make, shift, "tab", TO, "Bind::Press(\"shift tab\");");
  bindCommand(keyboard0, make, shift, "capslock", TO, "Bind::Press(\"shift capslock\");");
  bindCommand(keyboard0, make, shift, "backspace", TO, "Bind::Press(\"shift backspace\");");
  bindCommand(keyboard0, make, shift, "enter", TO, "Bind::Press(\"shift enter\");");
  bindCommand(keyboard0, make, shift, "insert", TO, "Bind::Press(\"shift insert\");");
  bindCommand(keyboard0, make, shift, "delete", TO, "Bind::Press(\"shift delete\");");
  bindCommand(keyboard0, make, shift, "home", TO, "Bind::Press(\"shift home\");");
  bindCommand(keyboard0, make, shift, "end", TO, "Bind::Press(\"shift end\");");
  bindCommand(keyboard0, make, shift, "prior", TO, "Bind::Press(\"shift prior\");");
  bindCommand(keyboard0, make, shift, "next", TO, "Bind::Press(\"shift next\");");
  bindCommand(keyboard0, make, shift, "space", TO, "Bind::Press(\"shift space\");");

  bindCommand(keyboard0, make, shift, "numpad0", TO, "Bind::Press(\"shift numpad0\");");
  bindCommand(keyboard0, make, shift, "numpad1", TO, "Bind::Press(\"shift numpad1\");");
  bindCommand(keyboard0, make, shift, "numpad2", TO, "Bind::Press(\"shift numpad2\");");
  bindCommand(keyboard0, make, shift, "numpad3", TO, "Bind::Press(\"shift numpad3\");");
  bindCommand(keyboard0, make, shift, "numpad4", TO, "Bind::Press(\"shift numpad4\");");
  bindCommand(keyboard0, make, shift, "numpad5", TO, "Bind::Press(\"shift numpad5\");");
  bindCommand(keyboard0, make, shift, "numpad6", TO, "Bind::Press(\"shift numpad6\");");
  bindCommand(keyboard0, make, shift, "numpad7", TO, "Bind::Press(\"shift numpad7\");");
  bindCommand(keyboard0, make, shift, "numpad8", TO, "Bind::Press(\"shift numpad8\");");
  bindCommand(keyboard0, make, shift, "numpad9", TO, "Bind::Press(\"shift numpad9\");");

  bindCommand(keyboard0, make, shift, "numpad/", TO, "Bind::Press(\"shift numpad/\");");
  bindCommand(keyboard0, make, shift, "numpad*", TO, "Bind::Press(\"shift numpad*\");");
  bindCommand(keyboard0, make, shift, "numpad-", TO, "Bind::Press(\"shift numpad-\");");
  bindCommand(keyboard0, make, shift, "numpad+", TO, "Bind::Press(\"shift numpad+\");");
  bindCommand(keyboard0, make, shift, "Decimal", TO, "Bind::Press(\"shift decimal\");");
  bindCommand(keyboard0, make, shift, "numpadenter", TO, "Bind::Press(\"shift numpadenter\");");

  bindCommand(keyboard0, make, shift, "up", TO, "Bind::Press(\"shift up\");");
  bindCommand(keyboard0, make, shift, "down", TO, "Bind::Press(\"shift down\");");
  bindCommand(keyboard0, make, shift, "left", TO, "Bind::Press(\"shift left\");");
  bindCommand(keyboard0, make, shift, "right", TO, "Bind::Press(\"shift right\");");

  bindCommand(keyboard0, make, alt, "tab", TO, "Bind::Press(\"alt tab\");");
  bindCommand(keyboard0, make, alt, "capslock", TO, "Bind::Press(\"alt capslock\");");
  bindCommand(keyboard0, make, alt, "backspace", TO, "Bind::Press(\"alt backspace\");");
  bindCommand(keyboard0, make, alt, "insert", TO, "Bind::Press(\"alt insert\");");
  bindCommand(keyboard0, make, alt, "delete", TO, "Bind::Press(\"alt delete\");");
  bindCommand(keyboard0, make, alt, "home", TO, "Bind::Press(\"alt home\");");
  bindCommand(keyboard0, make, alt, "end", TO, "Bind::Press(\"alt end\");");
  bindCommand(keyboard0, make, alt, "prior", TO, "Bind::Press(\"alt prior\");");
  bindCommand(keyboard0, make, alt, "next", TO, "Bind::Press(\"alt next\");");
  bindCommand(keyboard0, make, alt, "space", TO, "Bind::Press(\"alt space\");");

  bindCommand(keyboard0, make, alt, "numpad0", TO, "Bind::Press(\"alt numpad0\");");
  bindCommand(keyboard0, make, alt, "numpad1", TO, "Bind::Press(\"alt numpad1\");");
  bindCommand(keyboard0, make, alt, "numpad2", TO, "Bind::Press(\"alt numpad2\");");
  bindCommand(keyboard0, make, alt, "numpad3", TO, "Bind::Press(\"alt numpad3\");");
  bindCommand(keyboard0, make, alt, "numpad4", TO, "Bind::Press(\"alt numpad4\");");
  bindCommand(keyboard0, make, alt, "numpad5", TO, "Bind::Press(\"alt numpad5\");");
  bindCommand(keyboard0, make, alt, "numpad6", TO, "Bind::Press(\"alt numpad6\");");
  bindCommand(keyboard0, make, alt, "numpad7", TO, "Bind::Press(\"alt numpad7\");");
  bindCommand(keyboard0, make, alt, "numpad8", TO, "Bind::Press(\"alt numpad8\");");
  bindCommand(keyboard0, make, alt, "numpad9", TO, "Bind::Press(\"alt numpad9\");");

  bindCommand(keyboard0, make, alt, "numpad/", TO, "Bind::Press(\"alt numpad/\");");
  bindCommand(keyboard0, make, alt, "numpad*", TO, "Bind::Press(\"alt numpad*\");");
  bindCommand(keyboard0, make, alt, "numpad-", TO, "Bind::Press(\"alt numpad-\");");
  bindCommand(keyboard0, make, alt, "numpad+", TO, "Bind::Press(\"alt numpad+\");");
  bindCommand(keyboard0, make, alt, "Decimal", TO, "Bind::Press(\"alt decimal\");");

  bindCommand(keyboard0, make, alt, "up", TO, "Bind::Press(\"alt up\");");
  bindCommand(keyboard0, make, alt, "down", TO, "Bind::Press(\"alt down\");");
  bindCommand(keyboard0, make, alt, "left", TO, "Bind::Press(\"alt left\");");
  bindCommand(keyboard0, make, alt, "right", TO, "Bind::Press(\"alt right\");");

  bindCommand(keyboard0, make, control, "tab", TO, "Bind::Press(\"control tab\");");
  bindCommand(keyboard0, make, control, "capslock", TO, "Bind::Press(\"control capslock\");");
  bindCommand(keyboard0, make, control, "backspace", TO, "Bind::Press(\"control backspace\");");
  bindCommand(keyboard0, make, control, "enter", TO, "Bind::Press(\"control enter\");");
  bindCommand(keyboard0, make, control, "insert", TO, "Bind::Press(\"control insert\");");
  bindCommand(keyboard0, make, control, "delete", TO, "Bind::Press(\"control delete\");");
  bindCommand(keyboard0, make, control, "home", TO, "Bind::Press(\"control home\");");
  bindCommand(keyboard0, make, control, "end", TO, "Bind::Press(\"control end\");");
  bindCommand(keyboard0, make, control, "prior", TO, "Bind::Press(\"control prior\");");
  bindCommand(keyboard0, make, control, "next", TO, "Bind::Press(\"control next\");");
  bindCommand(keyboard0, make, control, "space", TO, "Bind::Press(\"control space\");");

  bindCommand(keyboard0, make, control, "numpad0", TO, "Bind::Press(\"control numpad0\");");
  bindCommand(keyboard0, make, control, "numpad1", TO, "Bind::Press(\"control numpad1\");");
  bindCommand(keyboard0, make, control, "numpad2", TO, "Bind::Press(\"control numpad2\");");
  bindCommand(keyboard0, make, control, "numpad3", TO, "Bind::Press(\"control numpad3\");");
  bindCommand(keyboard0, make, control, "numpad4", TO, "Bind::Press(\"control numpad4\");");
  bindCommand(keyboard0, make, control, "numpad5", TO, "Bind::Press(\"control numpad5\");");
  bindCommand(keyboard0, make, control, "numpad6", TO, "Bind::Press(\"control numpad6\");");
  bindCommand(keyboard0, make, control, "numpad7", TO, "Bind::Press(\"control numpad7\");");
  bindCommand(keyboard0, make, control, "numpad8", TO, "Bind::Press(\"control numpad8\");");
  bindCommand(keyboard0, make, control, "numpad9", TO, "Bind::Press(\"control numpad9\");");

  bindCommand(keyboard0, make, control, "numpad/", TO, "Bind::Press(\"control numpad/\");");
  bindCommand(keyboard0, make, control, "numpad*", TO, "Bind::Press(\"control numpad*\");");
  bindCommand(keyboard0, make, control, "numpad-", TO, "Bind::Press(\"control numpad-\");");
  bindCommand(keyboard0, make, control, "numpad+", TO, "Bind::Press(\"control numpad+\");");
  bindCommand(keyboard0, make, control, "Decimal", TO, "Bind::Press(\"control decimal\");");
  bindCommand(keyboard0, make, control, "numpadenter", TO, "Bind::Press(\"control numpadenter\");");

  bindCommand(keyboard0, make, control, "up", TO, "Bind::Press(\"control up\");");
  bindCommand(keyboard0, make, control, "down", TO, "Bind::Press(\"control down\");");
  bindCommand(keyboard0, make, control, "left", TO, "Bind::Press(\"control left\");");
  bindCommand(keyboard0, make, control, "right", TO, "Bind::Press(\"control right\");");


  bindCommand(keyboard0, make, shift, alt, "tab", TO, "Bind::Press(\"shift alt tab\");"); 
  bindCommand(keyboard0, make, shift, alt, "capslock", TO, "Bind::Press(\"shift alt capslock\");"); 
  bindCommand(keyboard0, make, shift, alt, "backspace", TO, "Bind::Press(\"shift alt backspace\");"); 
  bindCommand(keyboard0, make, shift, alt, "insert", TO, "Bind::Press(\"shift alt insert\");"); 
  bindCommand(keyboard0, make, shift, alt, "delete", TO, "Bind::Press(\"shift alt delete\");"); 
  bindCommand(keyboard0, make, shift, alt, "home", TO, "Bind::Press(\"shift alt home\");"); 
  bindCommand(keyboard0, make, shift, alt, "end", TO, "Bind::Press(\"shift alt end\");"); 
  bindCommand(keyboard0, make, shift, alt, "prior", TO, "Bind::Press(\"shift alt prior\");"); 
  bindCommand(keyboard0, make, shift, alt, "next", TO, "Bind::Press(\"shift alt next\");"); 
  bindCommand(keyboard0, make, shift, alt, "space", TO, "Bind::Press(\"shift alt space\");"); 

  bindCommand(keyboard0, make, shift, alt, "numpad0", TO, "Bind::Press(\"shift alt numpad0\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad1", TO, "Bind::Press(\"shift alt numpad1\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad2", TO, "Bind::Press(\"shift alt numpad2\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad3", TO, "Bind::Press(\"shift alt numpad3\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad4", TO, "Bind::Press(\"shift alt numpad4\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad5", TO, "Bind::Press(\"shift alt numpad5\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad6", TO, "Bind::Press(\"shift alt numpad6\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad7", TO, "Bind::Press(\"shift alt numpad7\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad8", TO, "Bind::Press(\"shift alt numpad8\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad9", TO, "Bind::Press(\"shift alt numpad9\");"); 

  bindCommand(keyboard0, make, shift, alt, "numpad/", TO, "Bind::Press(\"shift alt numpad/\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad*", TO, "Bind::Press(\"shift alt numpad*\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad-", TO, "Bind::Press(\"shift alt numpad-\");"); 
  bindCommand(keyboard0, make, shift, alt, "numpad+", TO, "Bind::Press(\"shift alt numpad+\");"); 
  bindCommand(keyboard0, make, shift, alt, "Decimal", TO, "Bind::Press(\"shift alt decimal\");"); 

  bindCommand(keyboard0, make, shift, alt, "up", TO, "Bind::Press(\"shift alt up\");"); 
  bindCommand(keyboard0, make, shift, alt, "down", TO, "Bind::Press(\"shift alt down\");"); 
  bindCommand(keyboard0, make, shift, alt, "left", TO, "Bind::Press(\"shift alt left\");"); 
  bindCommand(keyboard0, make, shift, alt, "right", TO, "Bind::Press(\"shift alt right\");"); 


  bindCommand(keyboard0, make, alt, control, "tab", TO, "Bind::Press(\"control alt tab\");"); 
  bindCommand(keyboard0, make, alt, control, "capslock", TO, "Bind::Press(\"control alt capslock\");"); 
  bindCommand(keyboard0, make, alt, control, "backspace", TO, "Bind::Press(\"control alt backspace\");"); 
  bindCommand(keyboard0, make, alt, control, "insert", TO, "Bind::Press(\"control alt insert\");"); 
  bindCommand(keyboard0, make, alt, control, "home", TO, "Bind::Press(\"control alt home\");"); 
  bindCommand(keyboard0, make, alt, control, "end", TO, "Bind::Press(\"control alt end\");"); 
  bindCommand(keyboard0, make, alt, control, "prior", TO, "Bind::Press(\"control alt prior\");"); 
  bindCommand(keyboard0, make, alt, control, "next", TO, "Bind::Press(\"control alt next\");"); 
  bindCommand(keyboard0, make, alt, control, "space", TO, "Bind::Press(\"control alt space\");"); 

  bindCommand(keyboard0, make, alt, control, "numpad0", TO, "Bind::Press(\"control alt numpad0\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad1", TO, "Bind::Press(\"control alt numpad1\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad2", TO, "Bind::Press(\"control alt numpad2\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad3", TO, "Bind::Press(\"control alt numpad3\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad4", TO, "Bind::Press(\"control alt numpad4\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad5", TO, "Bind::Press(\"control alt numpad5\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad6", TO, "Bind::Press(\"control alt numpad6\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad7", TO, "Bind::Press(\"control alt numpad7\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad8", TO, "Bind::Press(\"control alt numpad8\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad9", TO, "Bind::Press(\"control alt numpad9\");"); 

  bindCommand(keyboard0, make, alt, control, "numpad/", TO, "Bind::Press(\"control alt numpad/\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad*", TO, "Bind::Press(\"control alt numpad*\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad-", TO, "Bind::Press(\"control alt numpad-\");"); 
  bindCommand(keyboard0, make, alt, control, "numpad+", TO, "Bind::Press(\"control alt numpad+\");"); 

  bindCommand(keyboard0, make, alt, control, "up", TO, "Bind::Press(\"control alt up\");"); 
  bindCommand(keyboard0, make, alt, control, "down", TO, "Bind::Press(\"control alt down\");"); 
  bindCommand(keyboard0, make, alt, control, "left", TO, "Bind::Press(\"control alt left\");"); 
  bindCommand(keyboard0, make, alt, control, "right", TO, "Bind::Press(\"control alt right\");"); 


  bindCommand(keyboard0, make, shift, control, "tab", TO, "Bind::Press(\"control shift tab\");"); 
  bindCommand(keyboard0, make, shift, control, "capslock", TO, "Bind::Press(\"control shift capslock\");"); 
  bindCommand(keyboard0, make, shift, control, "backspace", TO, "Bind::Press(\"control shift backspace\");"); 
  bindCommand(keyboard0, make, shift, control, "enter", TO, "Bind::Press(\"control shift enter\");"); 
  bindCommand(keyboard0, make, shift, control, "insert", TO, "Bind::Press(\"control shift insert\");"); 
  bindCommand(keyboard0, make, shift, control, "delete", TO, "Bind::Press(\"control shift delete\");"); 
  bindCommand(keyboard0, make, shift, control, "home", TO, "Bind::Press(\"control shift home\");"); 
  bindCommand(keyboard0, make, shift, control, "end", TO, "Bind::Press(\"control shift end\");"); 
  bindCommand(keyboard0, make, shift, control, "prior", TO, "Bind::Press(\"control shift prior\");"); 
  bindCommand(keyboard0, make, shift, control, "next", TO, "Bind::Press(\"control shift next\");"); 
  bindCommand(keyboard0, make, shift, control, "space", TO, "Bind::Press(\"control shift space\");"); 

  bindCommand(keyboard0, make, shift, control, "numpad0", TO, "Bind::Press(\"control shift numpad0\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad1", TO, "Bind::Press(\"control shift numpad1\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad2", TO, "Bind::Press(\"control shift numpad2\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad3", TO, "Bind::Press(\"control shift numpad3\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad4", TO, "Bind::Press(\"control shift numpad4\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad5", TO, "Bind::Press(\"control shift numpad5\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad6", TO, "Bind::Press(\"control shift numpad6\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad7", TO, "Bind::Press(\"control shift numpad7\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad8", TO, "Bind::Press(\"control shift numpad8\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad9", TO, "Bind::Press(\"control shift numpad9\");"); 

  bindCommand(keyboard0, make, shift, control, "numpad/", TO, "Bind::Press(\"control shift numpad/\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad*", TO, "Bind::Press(\"control shift numpad*\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad-", TO, "Bind::Press(\"control shift numpad-\");"); 
  bindCommand(keyboard0, make, shift, control, "numpad+", TO, "Bind::Press(\"control shift numpad+\");"); 
  bindCommand(keyboard0, make, shift, control, "Decimal", TO, "Bind::Press(\"control shift decimal\");"); 
  bindCommand(keyboard0, make, shift, control, "numpadenter", TO, "Bind::Press(\"control shift numpadenter\");"); 

  bindCommand(keyboard0, make, shift, control, "up", TO, "Bind::Press(\"control shift up\");"); 
  bindCommand(keyboard0, make, shift, control, "down", TO, "Bind::Press(\"control shift down\");"); 
  bindCommand(keyboard0, make, shift, control, "left", TO, "Bind::Press(\"control shift left\");"); 
  bindCommand(keyboard0, make, shift, control, "right", TO, "Bind::Press(\"control shift right\");"); 

  bindCommand(keyboard0, make, shift, alt, control, "tab", TO, "Bind::Press(\"control alt shift tab\");");
  bindCommand(keyboard0, make, shift, alt, control, "capslock", TO, "Bind::Press(\"control alt shift capslock\");");
  bindCommand(keyboard0, make, shift, alt, control, "backspace", TO, "Bind::Press(\"control alt shift backspace\");");
  bindCommand(keyboard0, make, shift, alt, control, "insert", TO, "Bind::Press(\"control alt shift insert\");");
  bindCommand(keyboard0, make, shift, alt, control, "home", TO, "Bind::Press(\"control alt shift home\");");
  bindCommand(keyboard0, make, shift, alt, control, "end", TO, "Bind::Press(\"control alt shift end\");");
  bindCommand(keyboard0, make, shift, alt, control, "prior", TO, "Bind::Press(\"control alt shift prior\");");
  bindCommand(keyboard0, make, shift, alt, control, "next", TO, "Bind::Press(\"control alt shift next\");");
  bindCommand(keyboard0, make, shift, alt, control, "space", TO, "Bind::Press(\"control alt shift space\");");

  bindCommand(keyboard0, make, shift, alt, control, "numpad0", TO, "Bind::Press(\"control alt shift numpad0\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad1", TO, "Bind::Press(\"control alt shift numpad1\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad2", TO, "Bind::Press(\"control alt shift numpad2\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad3", TO, "Bind::Press(\"control alt shift numpad3\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad4", TO, "Bind::Press(\"control alt shift numpad4\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad5", TO, "Bind::Press(\"control alt shift numpad5\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad6", TO, "Bind::Press(\"control alt shift numpad6\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad7", TO, "Bind::Press(\"control alt shift numpad7\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad8", TO, "Bind::Press(\"control alt shift numpad8\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad9", TO, "Bind::Press(\"control alt shift numpad9\");");

  bindCommand(keyboard0, make, shift, alt, control, "numpad/", TO, "Bind::Press(\"control alt shift numpad/\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad*", TO, "Bind::Press(\"control alt shift numpad*\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad-", TO, "Bind::Press(\"control alt shift numpad-\");");
  bindCommand(keyboard0, make, shift, alt, control, "numpad+", TO, "Bind::Press(\"control alt shift numpad+\");");

  bindCommand(keyboard0, make, shift, alt, control, "up", TO, "Bind::Press(\"control alt shift up\");");
  bindCommand(keyboard0, make, shift, alt, control, "down", TO, "Bind::Press(\"control alt shift down\");");
  bindCommand(keyboard0, make, shift, alt, control, "left", TO, "Bind::Press(\"control alt shift left\");");
  bindCommand(keyboard0, make, shift, alt, control, "right", TO, "Bind::Press(\"control alt shift right\");");




	bindCommand(mouse0, make, button0, TO, "Bind::Press(\"button0\");");
	bindCommand(mouse0, make, button1, TO, "Bind::Press(\"button1\");");
	bindCommand(mouse0, make, button2, TO, "Bind::Press(\"button2\");");
	bindCommand(mouse0, make, button3, TO, "Bind::Press(\"button3\");");
	bindCommand(mouse0, make, button4, TO, "Bind::Press(\"button4\");");

	bindCommand(mouse0, zaxis0, TO, "Bind::Press(\"zaxis0\");");

	%chars   = "abcdefghijklmnopqrstuvwxyz1234567890-=[];',./\\";

	%i = 0;
	while((%char = String::getSubStr(%chars, %i, 1)) != "")
	{
		bindCommand(keyboard0, make, %char, TO, "Bind::Press(\""@ escapestring(%char) @"\");");
		bindCommand(keyboard0, make, shift, %char, TO, "Bind::Press(\"shift "@ escapestring(%char) @"\");");
		bindCommand(keyboard0, make, alt, %char, TO, "Bind::Press(\"alt "@ escapestring(%char) @"\");");
		bindCommand(keyboard0, make, control, %char, TO, "Bind::Press(\"control "@ escapestring(%char) @"\");");
		bindCommand(keyboard0, make, shift, alt, %char, TO, "Bind::Press(\"shift alt "@ escapestring(%char) @ "\");");
		bindCommand(keyboard0, make, alt, control, %char, TO, "Bind::Press(\"alt control "@ escapestring(%char) @"\");");
		bindCommand(keyboard0, make, shift, control, %char, TO, "Bind::Press(\"shift control "@ escapestring(%char) @"\");");
		bindCommand(keyboard0, make, shift, alt, control, %char, TO, "Bind::Press(\"shift alt control "@ escapestring(%char) @"\");");
		%i++;
	}

	for(%i=1;%i<=12;%i++)
	{
		bindCommand(keyboard0, make, "f"@%i, TO, "Bind::Press(\"f"@%i@"\");");
		bindCommand(keyboard0, make, shift, "f"@%i, TO, "Bind::Press(\"shift f"@%i@"\");");
		bindCommand(keyboard0, make, alt, "f"@%i, TO, "Bind::Press(\"alt f"@%i@"\");");
		bindCommand(keyboard0, make, control, "f"@%i, TO, "Bind::Press(\"control f"@%i@"\");");
		bindCommand(keyboard0, make, shift, alt, "f"@%i, TO, "Bind::Press(\"shift alt f"@%i@"\");");
		bindCommand(keyboard0, make, alt, control, "f"@%i, TO, "Bind::Press(\"alt control f"@%i@"\");");
		bindCommand(keyboard0, make, shift, control, "f"@%i, TO, "Bind::Press(\"shift control f"@%i@"\");");
		bindCommand(keyboard0, make, shift, alt, control, "f"@%i, TO, "Bind::Press(\"shift alt control f"@%i@"\");");
	}
  echo("BindMap.sae has been setup properly");
}
Bind::Setup();