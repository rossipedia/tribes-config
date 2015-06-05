function String::len(%string) {
    for(%length=0; String::getSubStr(%string, %length, 1) != ""; %length++) {} 
    return %length;
}

function getWordCount(%string)
{
  for (%i=0;getWord(%string,%i)!=-1;%i++) {}
  return %i;
}

function String::replace(%string, %search, %replace)    
{
  while ((%idx=String::FindSubStr(%string,%search))!=-1)
  {
    %left = %left @ String::GetSubStr(%string,0,%idx) @ %replace;
    %string = String::GetSubStr(%string,%idx+String::Len(%search),String::Len(%string));
  }
  %left = %left @ %string;
  return %left;
}

function String::UCase(%string) {
	%tmp = String::GetSubStr(%string, %i, 1);
	%lcase = "abcdefghijklmnopqrstuvwxyz1234567890 !@#$%^&*()-_=+,./<>?|[]{}";
	%ucase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 !@#$%^&*()-_=+,./<>?|[]{}";
	while(%tmp != "") {
		%idx = String::FindSubStr(%lcase, %tmp);
		%letter = String::GetSubStr(%ucase, %idx, 1);
		%ustring = %ustring@%letter;
		%i++;
		%tmp = String::GetSubStr(%string, %i, 1);
	}
	return %ustring;
}

function String::LCase(%string) {
	%tmp = String::GetSubStr(%string, %i, 1);
	%lcase = "abcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()-_=+,./<>?|[]{}";
	%ucase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+,./<>?|[]{}";
	while(%tmp != "") {
		%idx = String::FindSubStr(%ucase, %tmp);
		%letter = String::GetSubStr(%lcase, %idx, 1);
		%lstring = %lstring@%letter;
		%i++;
		%tmp = String::GetSubStr(%string, %i, 1);
	}
	return %lstring;
}

function String::Left(%string,%length)
{
	%len = String::len(%string);
	if (%length > %len)
		return %string;
	return String::GetSubStr(%string,0,%length);
}

function String::right(%string, %len)    
{
    if(%len >= String::len(%string))
        return %string;

    %idx = String::len(%string) - %len;
    %right = String::getSubStr(%string, %idx, %len);
    return %right;
}

function String::Pixels(%string) {
	%pixel = 0;
	%length = String::Len(%string);
	for (%i = 0; %i < %length; %i++) {
		%char = String::getSubStr(%string, %i, 1);
		%pos = String::findSubStr($String::asciiString, %char);
		if (%pos >= 0) {
			if (String::Compare(%char, String::getSubStr($String::asciiString, %pos, 1)) == 0)
				%pos = %pos + 32;
			else
				%pos = %pos + 64;
			%pixel = %pixel + $String::asciipixels[%pos];
		}
	}
	return %pixel;
}

$String::asciiString = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

$String::asciipixels[32] = 6;	//	space
$String::asciipixels[33] = 3;	//	!
$String::asciipixels[34] = 4;	//	"
$String::asciipixels[35] = 10;	//	#
$String::asciipixels[36] = 8;	//	$
$String::asciipixels[37] = 13;	//	%
$String::asciipixels[38] = 10;	//	&
$String::asciipixels[39] = 2;	//	'
$String::asciipixels[40] = 5;	//	(
$String::asciipixels[41] = 5;	//	)
$String::asciipixels[42] = 6;	//	*
$String::asciipixels[43] = 8;	//	+
$String::asciipixels[44] = 4;	//	,
$String::asciipixels[45] = 4;	//	-
$String::asciipixels[46] = 3;	//	.
$String::asciipixels[47] = 5;	//	/
$String::asciipixels[48] = 7;	//	0
$String::asciipixels[49] = 3;	//	1
$String::asciipixels[50] = 7;	//	2
$String::asciipixels[51] = 7;	//	3
$String::asciipixels[52] = 7;	//	4
$String::asciipixels[53] = 7;	//	5
$String::asciipixels[54] = 7;	//	6
$String::asciipixels[55] = 7;	//	7
$String::asciipixels[56] = 7;	//	8
$String::asciipixels[57] = 7;	//	9
$String::asciipixels[58] = 3;	//	:
$String::asciipixels[59] = 4;	//  ;
$String::asciipixels[60] = 8;	//	<
$String::asciipixels[61] = 9;	//	=
$String::asciipixels[62] = 8;	//	>
$String::asciipixels[63] = 5;	//	?
$String::asciipixels[64] = 13;	//	@
$String::asciipixels[65] = 11;	//	A
$String::asciipixels[66] = 8;	//	B
$String::asciipixels[67] = 9;	//	C
$String::asciipixels[68] = 10;	//	D
$String::asciipixels[69] = 7;	//	E
$String::asciipixels[70] = 7;	//	F
$String::asciipixels[71] = 10;	//	G
$String::asciipixels[72] = 9;	//	H
$String::asciipixels[73] = 3;	//	I
$String::asciipixels[74] = 5;	//	J
$String::asciipixels[75] = 10;	//	K
$String::asciipixels[76] = 7;	//	L
$String::asciipixels[77] = 11;	//	M
$String::asciipixels[78] = 10;	//	N
$String::asciipixels[79] = 11;	//	O
$String::asciipixels[80] = 8;	//	P
$String::asciipixels[81] = 11;	//	Q
$String::asciipixels[82] = 9;	//	R
$String::asciipixels[83] = 8;	//	S
$String::asciipixels[84] = 9;	//	T
$String::asciipixels[85] = 9;	//	U
$String::asciipixels[86] = 10;	//	V
$String::asciipixels[87] = 14;	//	W
$String::asciipixels[88] = 11;	//	X
$String::asciipixels[89] = 11;	//	Y
$String::asciipixels[90] = 10;	//	Z
$String::asciipixels[91] = 5;	//	[
$String::asciipixels[92] = 5;	//	\
$String::asciipixels[93] = 5;	//	]
$String::asciipixels[94] = 9;	//	^
$String::asciipixels[95] = 8;	//	_
$String::asciipixels[96] = 4;	//	`
$String::asciipixels[97] = 7;	//	a
$String::asciipixels[98] = 7;	//	b
$String::asciipixels[99] = 6;	//	c
$String::asciipixels[100] = 7;	//	d
$String::asciipixels[101] = 7;	//	e
$String::asciipixels[102] = 6;	//	f
$String::asciipixels[103] = 8;	//	g
$String::asciipixels[104] = 7;	//	h
$String::asciipixels[105] = 3;	//	i
$String::asciipixels[106] = 4;	//	j
$String::asciipixels[107] = 8;	//	k
$String::asciipixels[108] = 3;	//	l
$String::asciipixels[109] = 11;	//	m
$String::asciipixels[110] = 7;	//	n
$String::asciipixels[111] = 8;	//	o
$String::asciipixels[112] = 7;	//	p
$String::asciipixels[113] = 7;	//	q
$String::asciipixels[114] = 6;	//	r
$String::asciipixels[115] = 6;	//	s
$String::asciipixels[116] = 6;	//	t
$String::asciipixels[117] = 7;	//	u
$String::asciipixels[118] = 8;	//	v
$String::asciipixels[119] = 11;	//	w
$String::asciipixels[120] = 8;	//	x
$String::asciipixels[121] = 8;	//	y
$String::asciipixels[122] = 7;	//	z
$String::asciipixels[123] = 6;	//	{
$String::asciipixels[124] = 2;	//	|
$String::asciipixels[125] = 6;	//	}
$String::asciipixels[126] = 9;	//	~


function String::GetLines(%string,%width)
{
  %pixel = 0;
	%length = String::Len(%string);
  %lines = 1;
	for (%i = 0; %i < %length; %i++) {
		%char = String::getSubStr(%string, %i, 1);
		%pos = String::findSubStr($String::asciiString, %char);
		if (%pos >= 0) {
			if (String::Compare(%char, String::getSubStr($String::asciiString, %pos, 1)) == 0)
				%pos = %pos + 32;
			else
				%pos = %pos + 64;
			%pixel = %pixel + $String::asciipixels[%pos];
      if (%pixel > %width)
      {
        %lines++;
        %pixel = 0;
      }
		}
	}
  return %lines;
}



function String::LTrim(%string)
{
	for (%i=0;String::GetSubStr(%string,%i,1)== " ";%i++) {}
	return String::GetSubStr(%string,%i,1024);
}

function String::RTrim(%string)
{
	%cur = String::Len(%string)-1;
	while (String::GetSubStr(%string,%cur,1)==" ")
		%cur--;
	return String::GetSubStr(%string,0,%cur+1);
}

function String::Trim(%string)
{
	return String::RTrim(String::LTrim(%string));
}

function String::Slice(%string,%substr)
{
	if ((%idx = String::FindSubStr(%string,%substr)) == -1)
		return %string;
	else
	{
		%left = String::GetSubStr(%string,0,%idx);
		%lensub = String::Len(%substr);
		%idxstart = %idx+%lensub;
		%right = String::GetSubStr(%string,%idxstart,1024);
		return %left @ %right;
	}
}

function String::Stuff(%string,%substr,%idx,%replace)
{
	if (%replace < 0) %replace = 0;
	%left = String::GetSubStr(%string,0,%idx);
	%right = String::GetSubStr(%string,%idx+%replace,1024);
	return %left @ %substr @ %right;
}

function String::Split(%string,%delimiter,%arrayname)
{
  if (%delimiter=="" || %arrayname == "") { return; }
  %len = String::Len(%string);
  %dLen = String::Len(%delimiter);
  %idx = 0;
  while ((%ind = String::FindSubStr(%string,%delimiter))!=-1)
  {
    %idx++;
    %val = String::GetSubStr(%string,0,%ind);
    %string = String::GetSubStr(%string,%ind+%dLen,%len);
    eval("$"@%arrayname@"[%idx] = \""@%val@"\";");
    if (%count>=100) break;
  }
  eval("$"@%arrayname@"[%idx+1] = \""@%string@"\";");
  return %idx+1;
}


function String::AddMatch(%txt)
{
	$String::Matches[$String::NumMatches] = %txt;
	$String::NumMatches++;
}

function String::ClearMatches()
{
	for (%i=0;$String::Matches[%i]!="";%i++)
		$String::Matches[%i] = "";
	$String::NumMatches = 0;
}

function String::MatchResult(%num)
{
	return $String::Matches[%num];
}

function String::Match(%str,%srch,%wildcard)
{
	String::ClearMatches();
	if (%wildcard == "")
		%wildcard = "*";
	%lenwc = String::Len(%wildcard);
	return String::_Match(%str,%srch,%wildcard,%lenwc);
}

function String::_Match(%str,%srch,%wildcard,%lenwc)
{
	%lentext = String::FindSubStr(%srch,%wildcard);
	if (%lentext == -1)
		return (%str == %srch);
	%pretext = String::GetSubStr(%srch,0,%lentext);
	if (%pretext != String::GetSubStr(%str,0,String::Len(%pretext)))
		return "FALSE";
	%chop = String::GetSubStr(%str,String::Len(%pretext),String::Len(%str));
	%posttext = String::GetSubStr(%srch,String::Len(%pretext)+%lenwc,String::Len(%srch));
	%chktext = %posttext;
	if (String::FindSubStr(%chktext,%wildcard) != -1) 
	{
		%chktext = String::GetSubStr(%chktext,0,String::FindSubStr(%chktext,%wildcard));
		%callagain = true;
	}
	else 
		%callagain = "FALSE";
	if (String::Len(%chktext) > 0) {
		%indchk = String::FindSubStr(%chop,%chktext);
		if (%indchk <= 0)
			return "FALSE";
		%mtch = String::GetSubStr(%chop,0,%indchk);
	}
	else
		%mtch = %chop;
	String::AddMatch(%mtch);
	%chop = String::GetSubStr(%chop,String::Len(%mtch),String::Len(%chop));
	if(%callagain) 
  {
    %tmp = String::_Match(%chop,%posttext,%wildcard,%lenwc);
    if (!%tmp)
      return "FALSE";
	}
	return $String::NumMatches;
}

function String::NumMatches()
{
	return $String::NumMatches;
}


function Schedule::Add(%eval, %time, %tag)
{
    if(%tag == "")
        %tag = %eval;
	Schedule::Cancel(%tag);
	schedule("Schedule::Do(\"" @ escapestring(%eval) @ "\"," @ $Schedule::id[%tag] @ ",\"" @ escapestring(%tag) @ "\");", %time);
}

function Schedule::Do(%eval, %id, %tag)
{
    if($Schedule::id[%tag] != %id) 
			return;
    Schedule::Cancel(%tag);
    eval(%eval);
}

function Schedule::Cancel(%tag)
{
    $Schedule::id[%tag]++;
    if($Schedule::id[%tag] > 32767)
        $Schedule::id[%tag] = 0;
}

function Interval::Start(%command,%timeout,%id)
{
	if(%id=="") %id=%command;
	Schedule::Add("Interval::cont(\"" @ escapestring(%command) @ "\"," @ %timeout @ ",\"" @ escapestring(%id) @ "\");",%timeout,%id);
}

function Interval::cont(%command,%timeout,%id)
{
	eval(%command);
	Schedule::Add("Interval::cont(\"" @ escapestring(%command) @ "\"," @ %timeout @ ",\"" @ escapestring(%id) @ "\");",%timeout,%id);
}

function Interval::Cancel(%id)
{
	Schedule::Cancel(%id);
}



function iif(%test,%true,%false)
{
	if(%test)
		return %true;
	else
		return %false;
} 

function IsFlooded(%tag,%time,%max)
{
  if (%max=="")
    %max = 1;
  if ($Flooded::[%tag]+0<%max)
  {
    $Flooded::[%tag]++;
    if (%time=="")
      %time = 5;
    Schedule::Add("$Flooded::[\""@escapestring(%tag)@"\"]--;",%time);
    return "FALSE";
  }
  return "TRUE";
}

function Tap::Start(%id,%time)
{
	if (%time == "")
	{
		echo("ERROR: Must specify tap time. id: "@%id);
		return;
	}
	$Tap::tapped[%id]="TRUE";
	Schedule::Add("Tap::stop("@%id@");",%time,%id);
}

function Tap::stop(%id)
{
	$Tap::tapped[%id]="";
}

function Tap::Cancel(%id)
{
	if (%id=="")
	{
		echo("ERROR: Must specify tap id to cancel.");
		return;
	}	
	Schedule::Cancel(%id);
}

function tapped(%id)
{
	return $Tap::tapped[%id];
}


function DoubleTap::Start(%id,%taptime)
{
	if (tapped(%id@_dbl))
	{
		$DblTap::[%id]="TRUE";
		Schedule::Add("$DblTap::["@%id@"]=\"\";",%taptime,%id);
	}
	else
	{
		tap::Start(%id@_dbl,%taptime);
	}	
}

function DoubleTap::Cancel(%id)
{
	if (%id=="")
	{
		echo("ERROR: Must specify double tap id to cancel.");
		return;
	}	
	Schedule::Cancel(%id);
}

function DoubleTapped(%id)
{
	return $DblTap::[%id];
}

function getScreenSize(%coord)
{
	if ($pref::VideoFullScreen)
		%scr = String::Replace($pref::videofullscreenres,"x"," ");
	else
		%scr = "640 480";
	if (%coord=="") 
		return %scr;
	else if (%coord==x)
		return getWord(%scr,0);
	else if (%coord==y)
		return getWord(%scr,1);
	else
		echo("Unknown coordinate for screensize: "@%coord);
}

function Toggle::Var(%variable,%description)
{
	%str = "if ("@%variable@") "@%variable@" = \"FALSE\"; else "@%variable@"=\"TRUE\";";
	eval(%str);
	eval("%check = " @ %variable @ ";");
	if(%check)
		%state = "ON";
	else
		%state = "OFF";
	if(%description !="")
		remoteBP(2048, "<jc>"@%description @ " is: <f2>" @ %state,3);
}

function String::Validate(%string)
{
  %invalidchars = "\/:*?\"<>| ";
  for (%i=0;(%char=String::GetSubStr(%invalidchars,%i,1))!="";%i++)
  {
    if (String::FindSubStr(%string,%char)!=-1)
      %string=String::Replace(%string,%char,"_");
  }
  return %string;
}


function Prompt(%question,%default,%exec,%cancel,%loc)
{
  if (%loc=="")
    %loc = "CP";
  $Prompt::Loc = %loc;
  $Prompt::Question = %question;
  $Prompt::Exec = %exec;
  $Prompt::Cancel = %cancel;
  $Prompt::Text = %default;
  if ($Prompt::Setup!="TRUE")
    Prompt::Setup();
  pushActionMap("promptMap.sae");
  Prompt::Display();
  $Prompt::Active = "TRUE";
}

function Prompt::Display()
{
  eval("remote"@$Prompt::Loc@"(2048, \"<jc>\" @ $Prompt::Question @\"\\n\\n<f2>\" @ escapestring($Prompt::Text) @ \"\\n\\n<f1>Begin Typing\");");
  Schedule::Add("Prompt::Display();",1,PROMPT_ACTIVE);
}

function Prompt::Press(%char)
{
	if (%char == escape)
	{
		popActionMap("promptMap.sae");
    Schedule::Cancel(PROMPT_ACTIVE);
		remoteCP(2048, "");
		if($Prompt::Cancel == "")
			return;
		if(String::FindSubStr($Prompt::Cancel,";") != -1)
			eval($Prompt::Cancel);
		else
			eval($Prompt::Cancel@"(\""@$Prompt::Text@"\");");
    deleteVariables("Prompt::*");
		return;
	}
	if (%char == enter)
	{
		popActionMap("promptMap.sae");
    Schedule::Cancel(PROMPT_ACTIVE);
		remoteCP(2048, "");
		if($Prompt::Exec== "")
			return;
		if(String::FindSubStr($Prompt::Exec,";") != -1)
			eval($Prompt::Exec);
		else
			eval($Prompt::Exec@"(\""@$Prompt::Text@"\");");
    deleteVariables("Prompt::*");
		return;
	}
	if (%char == backspace)
		$Prompt::Text = String::GetSubStr($Prompt::Text,0,String::Len($Prompt::Text)-1);
	else
		$Prompt::Text = $Prompt::Text@%char;
	
  Prompt::Display();
	
  if(%char == $Prompt::LastChar)
		Schedule::Add("Prompt::press(\""@escapestring(%char)@"\");",0.05,PromptKeyRepeat);
	else
		Schedule::Add("Prompt::press(\""@escapestring(%char)@"\");",0.5,PromptKeyRepeat);
	
  $Prompt::LastChar = %char;
}

function Prompt::release(%char)
{
	$Prompt::LastChar = "";
	Schedule::Cancel(PromptKeyRepeat);
}

function Prompt::setup()
{
	%map = "promptMap.sae";

	NewActionMap(%map);
	
	bindCommand(keyboard0, make, "backspace", TO, "Prompt::press(backspace);");
	bindCommand(keyboard0, break, "backspace", TO, "Prompt::release(backspace);");
	bindCommand(keyboard0, make, "enter", TO, "Prompt::press(enter);");
	bindCommand(keyboard0, break, "enter", TO, "Prompt::release(enter);");
	bindCommand(keyboard0, make, "escape", TO, "Prompt::press(escape);");
	bindCommand(keyboard0, break, "escape", TO, "Prompt::release(escape);");
	bindCommand(keyboard0, make, "space", TO, "Prompt::press(\" \");");
	bindCommand(keyboard0, break, "space", TO, "Prompt::release(\" \");");

	bindCommand(keyboard0, make, "numpad+", TO, "Prompt::press(\"+\");");
	bindCommand(keyboard0, break, "numpad+", TO, "Prompt::release(\"+\");");
	bindCommand(keyboard0, make, "numpad-", TO, "Prompt::press(\"-\");");
	bindCommand(keyboard0, break, "numpad-", TO, "Prompt::release(\"-\");");
	bindCommand(keyboard0, make, "numpadenter", TO, "Prompt::press(enter);");
	bindCommand(keyboard0, break, "numpadenter", TO, "Prompt::release(enter);");
	bindCommand(keyboard0, make, "numpad/", TO, "Prompt::press(\"/\");");
	bindCommand(keyboard0, break, "numpad/", TO, "Prompt::release(\"/\");");
	bindCommand(keyboard0, make, "numpad*", TO, "Prompt::press(\"*\");");
	bindCommand(keyboard0, break, "numpad*", TO, "Prompt::release(\"*\");");
	bindCommand(keyboard0, make, "numpad1", TO, "Prompt::press(1);");
	bindCommand(keyboard0, break, "numpad1", TO, "Prompt::release(1);");
	bindCommand(keyboard0, make, "numpad2", TO, "Prompt::press(2);");
	bindCommand(keyboard0, break, "numpad2", TO, "Prompt::release(2);");
	bindCommand(keyboard0, make, "numpad3", TO, "Prompt::press(3);");
	bindCommand(keyboard0, break, "numpad3", TO, "Prompt::release(3);");
	bindCommand(keyboard0, make, "numpad4", TO, "Prompt::press(4);");
	bindCommand(keyboard0, break, "numpad4", TO, "Prompt::release(4);");
	bindCommand(keyboard0, make, "numpad5", TO, "Prompt::press(5);");
	bindCommand(keyboard0, break, "numpad5", TO, "Prompt::release(5);");
	bindCommand(keyboard0, make, "numpad6", TO, "Prompt::press(6);");
	bindCommand(keyboard0, break, "numpad6", TO, "Prompt::release(6);");
	bindCommand(keyboard0, make, "numpad7", TO, "Prompt::press(7);");
	bindCommand(keyboard0, break, "numpad7", TO, "Prompt::release(7);");
	bindCommand(keyboard0, make, "numpad8", TO, "Prompt::press(8);");
	bindCommand(keyboard0, break, "numpad8", TO, "Prompt::release(8);");
	bindCommand(keyboard0, make, "numpad9", TO, "Prompt::press(9);");
	bindCommand(keyboard0, break, "numpad9", TO, "Prompt::release(9);");
	bindCommand(keyboard0, make, "numpad0", TO, "Prompt::press(0);");
	bindCommand(keyboard0, break, "numpad0", TO, "Prompt::release(0);");


	%chars   = "abcdefghijklmnopqrstuvwxyz1234567890-=[];',./\\";
	%shifted = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+{}:\"<>?|";

	%i = 0;
	while((%char = String::getSubStr(%chars, %i, 1)) != "")
	{
			bindCommand(keyboard0, make, %char, TO, "Prompt::press(\"" @ escapestring(%char) @ "\");");
			bindCommand(keyboard0, break, %char, TO, "Prompt::release(\"" @ escapestring(%char) @ "\");");
			bindCommand(keyboard0, make, shift, %char, TO, "Prompt::press(\"" @ escapestring(String::getSubStr(%shifted, %i, 1)) @ "\");");
			bindCommand(keyboard0, break, shift, %char, TO, "Prompt::release(\"" @ escapestring(String::getSubStr(%shifted, %i, 1)) @ "\");");
			%i++;
	}
  $Prompt::Setup="TRUE";
}

function Confirm(%question,%onYes,%onNo)
{
  $Confirm::Question = %question;
  $Confirm::Yes = %onYes;
  $Confirm::No = %onNo;
  if ($Confirm::Setup!="TRUE")
  {
    NewActionMap("confirmMap.sae");
    bindCommand(keyboard, make, y, TO, "Confirm::Answer(Yes);");
    bindCommand(keyboard, make, enter, TO, "Confirm::Answer(Yes);");
    bindCommand(keyboard, make, numpadenter, TO, "Confirm::Answer(Yes);");
    bindCommand(keyboard,make, n, TO, "Confirm::Answer(No);");
    bindCommand(keyboard,make, escape, TO, "Confirm::Answer(No);");
    $Confirm::Setup="TRUE";
  }
  pushActionMap("confirmMap.sae");
  Confirm::Check();
}

function Confirm::Check()
{
  Client::CenterPrint("<jc><f0>"@$Confirm::Question@"\n\n<f2><jc>Y/Enter for Yes         N/Escape for No",0);
  Schedule::Add("Confirm::Check();",1,CONFIRM_UP);
}

function Confirm::Answer(%answer)
{
  Schedule::Cancel(CONFIRM_UP);
  popActionMap("confirmMap.sae");
  Client::CenterPrint("",0);
  if ($Confirm::[%answer]!="")
    eval($Confirm::[%answer]);
}

function IsNum(%num)
{
  if (%num=="") return "FALSE";
  %nums = "-0123456789.";
  for (%i=0;(%char=String::GetSubStr(%num,%i,1))!="";%i++)
  {
    if (String::FindSubStr(%nums,%char)==-1)
      return "FALSE";
  }
  return "TRUE";
}

function abs(%num)
{
  if (!IsNum(%num))
    return %num;
  if (%num<0)
    %num = -%num;
  return %num;
}

function getPercent(%num,%percent)
{
  %percent = String::Slice(%percent,"%");
  if (!IsNum(%num) || !IsNum(%percent))
  {
    echo("getPercent(): Invalid parameter passed.");
    return 0;
  }
  return %num * (%percent/100);
}


function LoadFolder(%folder,%ext)
{
  if (%folder!="") 
    %folder = %folder @ "\\"; 
  
  if (%ext=="") 
    %ext = "*.cs"; 

  %file = File::FindFirst(%folder@%ext);
  while (%file!="")
  {
    if ($daerid::exclude[File::GetBase(%file)] != "TRUE")
      Include(%file);  
    %file = File::FindNext(%folder@%ext);
  }
}

function Exclude(%file)
{
  $daerid::exclude[%file] = "TRUE";
}

function Include(%file,%force)
{
  if (String::FindSubStr(%file,".cs")==-1)
    %file = %file @ ".cs";

  if (File::FindFirst(%file)=="")
  {
    echo("File Not found: "@%file);
    return;
  }
  if (!$Included[%file] || %force)
  {
    exec(%file);
    $Included[%file]++;
  }
}

function Included(%file)
{
  if (String::FindSubStr(%file,".cs")==-1)
    %file = %file @ ".cs";
  return $Included[%file];
}



function Event::Attach(%event,%cmd)
{
  $Event::Statement[%cmd] = (String::FindSubStr(%cmd,";")!=-1);
  for (%i=1;(%func = $Event::Cmd[%event,%i])!="";%i++)
  {
    if (%func==%cmd)
      return;
  }
  $Event::Cmd[%event,%i] = %cmd;
}

function Event::Detach(%event,%cmd)
{
  for (%i=1;(%func = $Event::Cmd[%event,%i])!="";%i++)
  {
    if (%func == %cmd)
      break;
  }
  while ($Event::Cmd[%event,%i+1]!="")
  {
    $Event::Cmd[%event,%i] = $Event::Cmd[%event,%i+1];
    %i++;
  }
  $Event::Cmd[%event,%i] = "";
}

function Event::Trigger(%event,%p0,%p1,%p2,%p3,%p4,%p5,%p6,%p7,%p8,%p9)
{
  //echo("Triggering Event: "@%event);
  $Event::Current = %event; // So we can track what event is currently being triggered.
  for (%i=1;(%cmd = $Event::Cmd[%event,%i])!="";%i++)
  {
    if ($Event::Statement[%cmd])
      %ret = eval(%cmd);
    else
      %ret = eval(%cmd@"(%p0,%p1,%p2,%p3,%p4,%p5,%p6,%p7,%p8,%p9);");
    if (%return!="FALSE")
      %return = %ret;
  }
  $Event::Current = "";
  return %return;
}

function Event::List(%event)
{
  for (%i=1;(%cmd = $Event::Cmd[%event,%i])!="";%i++)
    echo(%cmd);
}

// ==========================================================================
// Message CallBacks
function MSGCB::Add(%msg,%cmd)
{
  for (%i=1;$MSGCB[%msg,%i]!="";%i++)
  {
    if ($MSGCB[%msg,%i]==%cmd)
      return;
  }
  //echo("Adding \""@%cmd@"\" to msg: \""@%msg@"\"");
  $MSGCB[%msg,%i] = %cmd;
}

function MSGCB::Remove(%msg,%cmd)
{
  for (%i=1;$MSGCB[%msg,%i]!="";%i++)
  {
    if ($MSGCB[%msg,%i]==%cmd)
      break;
  }
  //if ($MSGCB[%msg,%i]!="") echo("Removing \""@%cmd@"\" from msg: \""@%msg@"\"");
  while ($MSGCB[%msg,%i+1]!="")
  {
    $MSGCB[%msg,%i] = $MSGCB[%msg,%i+1];
    %i++;
  }
  $MSGCB[%msg,%i] = "";
}
// ==========================================================================

// ============================================================================
// Ping Functions
function Ping::Get()
{
  $Ping::Start = getSimTime();
  remoteEval( 2048, "Eval", PingGet );
}


function remotePingGet( %client )
{
  %ping = getSimTime() - $Ping::Start;
  Event::Trigger( EventPing, %ping );
}
// ============================================================================