// ==========================================================================
// Mini Demo Hud. Just a small, text-based demo-hud I built for [POE]Cauthon

// Used to toggle the display of the hud on/off
$MDH::ToggleKey = "backspace";

// The range of speeds
$MDH::Scale[0] = "0";
$MDH::Scale[1] = "0.1";
$MDH::Scale[2] = "0.25";
$MDH::Scale[3] = "0.5";
$MDH::Scale[4] = "1";
$MDH::Scale[5] = "2";
$MDH::Scale[6] = "3";
$MDH::Scale[7] = "4";
$MDH::Scale[8] = "5";
$MDH::Scale[9] = "6";
$MDH::Scale[10] = "8";
$MDH::Scale[11] = "12";
$MDH::Scale[12] = "16";
$MDH::Scale[13] = "20";
$MDH::Scale[14] = "24";
$MDH::Scale[15] = "32";
// ==========================================================================
bind($MDH::ToggleKey,"MDH::Toggle();");

$MDH::Current = 4;

Event::Attach(EventPlayMode,MDH::Create);

function MDH::Create()
{
  if (!$PlayingDemo) 
  {
    MDH::Destroy(); 
    $SimGame::TimeScale = 1;
  }
  else
  {
    if (!$MDH::Created)
    {
      MDH::CreateHUD();
      $MDH::Created = "TRUE";
      MDH::Check();
    }
  }
}

function MDH::Destroy(%gui)
{
  if ($MDH::Created)
  {
   MDH::DestroyHUD();
   $MDH::Created = "";
  }
}

function MDH::CreateHud()
{
  if (!$PlayingDemo) return;
  $MDH::Container = newObject(MiniDemoHud,SimGui::Control,0,400,100,34);
  $MDH::Frame = newObject(MiniDemoFrame,FearGui::FearGuiMenu,0,0,100,34);
  $MDH::Box = newObject(MiniDemoHudBox,FearGui::FearGuiBox,0,0,100,34);
  $MDH::Text = newObject(MiniDemoHudText,FearGuiFormattedText,0,0,100,34);

  addToSet($MDH::Container,$MDH::Frame);
  addToSet($MDH::Container,$MDH::Box);
  addToSet($MDH::Container,$MDH::Text);

  Control::SetValue(MiniDemoHudText, "<JC>MiniDemoHud\n<f1><<<<  <f1>x  <f2>>  <f1>>>");

  $MDH::SDButton = newObject(MiniDemoHud_SD,FearGui::FGUniversalButton, 8, 18, 17, 13, "", "MDH::SetSpeed(slow);");
  $MDH::PauseButton = newObject(MiniDemoHud_Pause,FearGui::FGUniversalButton, 30, 18, 17, 13, "", "MDH::SetSpeed(pause);");
  $MDH::PlayButton = newObject(MiniDemoHud_Play,FearGui::FGUniversalButton, 48, 18, 17, 13, "", "MDH::SetSpeed(play);");
  $MDH::FFButton = newObject(MiniDemoHud_FF,FearGui::FGUniversalButton, 72, 18, 17, 13, "", "MDH::SetSpeed(fast);");

  addToSet($MDH::Container,$MDH::SDButton);
  addToSet($MDH::Container,$MDH::PauseButton);
  addToSet($MDH::Container,$MDH::PlayButton);
  addToSet($MDH::Container,$MDH::FFButton);

  addToSet(PlayGui,$MDH::Container);

}

function MDH::Check()
{
  if ($MDH::Created)
  {
    cursorOn(MainWindow);
    schedule("MDH::Check();",1);
  }
  else
  {
    cursorOff(MainWindow);
  }
}

function MDH::DestroyHUD()
{
  removeFromSet(PlayGui, $MDH::Container);
  deleteObject($MDH::Container);
}

function MDH::SetSpeed(%dir)
{
  if (%dir == slow)
  {
    $MDH::Current--;
    if ($MDH::Current<0)
      $MDH::Current = 0;
    %text = "<JC>MiniDemoHud\n<f2><<<<  <f1>x  <f1>>  <f1>>>";
  }
  else if (%dir == pause)
  {
    $MDH::Current = 0;    
    %text = "<JC>MiniDemoHud\n<f1><<<<  <f2>x  <f1>>  <f1>>>";
  }
  else if (%dir == play)
  {
    $MDH::Current = 4;
    %text = "<JC>MiniDemoHud\n<f1><<<<  <f1>x  <f2>>  <f1>>>";
  }
  else if (%dir == fast)
  {
    $MDH::Current++;
    if ($MDH::Current>15)
      $MDH::Current = 15;
    %text = "<JC>MiniDemoHud\n<f1><<<<  <f1>x  <f1>>  <f2>>>";
  }
  $SimGame::TimeScale = $MDH::Scale[$MDH::Current];
  Control::SetValue(MiniDemoHudText,%text);
  remoteBP(2048, "Setting Playback speed to : "@$MDH::Scale[$MDH::Current]@" x", (3*$MDH::Scale[$MDH::Current]));
}


function MDH::Toggle()
{
  if (!$PlayingDemo)
    return;
  Control::SetVisible(MiniDemoHud,!Control::GetVisible(MiniDemoHud));
}