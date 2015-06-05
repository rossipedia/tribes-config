////////////////////////////////////////////////////////////
// File:	Connect.cs
// Version:	1.0
// Author:	Runar
// Credits:	|HH|BigBunny
// Info:	JoinGame Retry. Basically the same as in NewInterface
//		Added a check for Password check.
//
//
////////////////////////////////////////////////////////////

newObject("Viking", SimVolume, File::findFirst("*Connect.vol"));

$Viking::Connect = "False";
$Viking::Failed = "False";

Event::Attach(eventConnectionRejected, Viking::checkRejoin);
Event::Attach(eventConnectionAccepted, Viking::cancelRetry);

function Viking::Connect()
{
	$quitOnDisconnect	= "False";
	$Viking::Connect = "True";
	GuiLoadContentCtrl(MainWindow, "gui\\Loading.gui");
	connect($Server::Address);
}

function Viking::CheckReJoin()
{
	if($Viking::Connect)
	{
		if(string::findsubstr($errorString, "Wrong Password") != -1)
		{
			return;
		}
		else
		{
			$Viking::Failed = "True";
			Viking::ReConnect();
		}
	}
}

function Viking::ReConnect()
{
	if($Viking::Failed)
	{
		GuiLoadContentCtrl(MainWindow, "gui\\Connect.gui");
		Schedule::Add("GuiPopDialog(MainWindow, 0);", 0.1);
		Schedule::Add("GuiPushDialog(MainWindow, \"gui\\\\MsgRetryDlg.gui\");", 0.15);
		Schedule::Add("Control::setValue(MessageDialogTextFormat, \"<jc><f2>Failed to join server.\\n\\nWill auto-retry in 5 seconds...\");", 0.2);
		Schedule::Add("Control::setValue(MessageDialogTextFormat, \"<jc><f2>Failed to join server.\\n\\nWill auto-retry in 4 seconds...\");", 1.2);
		Schedule::Add("Control::setValue(MessageDialogTextFormat, \"<jc><f2>Failed to join server.\\n\\nWill auto-retry in 3 seconds...\");", 2.2);
		Schedule::Add("Control::setValue(MessageDialogTextFormat, \"<jc><f2>Failed to join server.\\n\\nWill auto-retry in 2 seconds...\");", 3.2);
		Schedule::Add("Control::setValue(MessageDialogTextFormat, \"<jc><f2>Failed to join server.\\n\\nWill auto-retry in 1 seconds...\");", 4.2);
		Schedule::Add("Viking::Connect();", 5);
	}
}

function Viking::CancelRetry()
{
	if($Viking::Failed)
	{
		Schedule::Cancel("Viking::Connect();");
		GuiLoadContentCtrl(MainWindow, "gui\\joinGame.gui");
	}

	$Viking::Connect = "False";
	$Viking::Failed = "False";
}

function MessageRetryReturnHandling()
{
		Viking::CancelRetry();
}
