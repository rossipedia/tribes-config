// =================================================================
// Demo Dropping Functionality

$DD::StartKey = "alt home";
$DD::StopKey = "alt end";

bind($DD::StartKey,"DD::Start();","",DemoDropStart);
bind($DD::StopKey,"DD::Stop();","",DemoDropEnd);


function DD::Start() {
	Prompt("Enter the name of this Demo: ","",DD::SetupDemo);	
}

function DD::StartNow(%text)
{
    say(0, "Demo Drop... BRB.~wwait1");
		schedule("DD::SetupDemo(\""@escapestring(%text)@"\");", 3);
}

function DD::Stop() {
    DD::StopDemo();
    return;
}

function DD::SetupDemo(%name) {
	$ConnectedToServer = FALSE;
	setCursor(MainWindow, "Cur_Arrow.bmp");
	disconnect();
	deleteObject(ConsoleScheduler);
	newObject(ConsoleScheduler, SimConsoleScheduler);
	cursorOn(MainWindow);
	$recordDemo = true;
	setupRecorderFile(%name);
	connect($Server::Address);
}



function DD::StopDemo()
{
	$ConnectedToServer = FALSE;
	setCursor(MainWindow, "Cur_Arrow.bmp");
	disconnect();
	deleteObject(ConsoleScheduler);
	newObject(ConsoleScheduler, SimConsoleScheduler);
	cursorOn(MainWindow);
	$recordDemo = false;
	$recorderFileName = "";
	connect($Server::Address);
}
// =================================================================