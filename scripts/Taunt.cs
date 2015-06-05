// ==========================================================================
// Auto-Taunt. Hey, why not? Taunts on kills, and you can taunt
// manually by hitting this key ;)
$AT::Key = "n";

// Here are the taunt wavs
$AT::Message[1] = cheer3;
$AT::Message[2] = taunt4;
$AT::Message[3] = taunt3;
$AT::Message[4] = dsgst1;
$AT::Message[5] = wshoot1;
$AT::Message[6] = hello;
$AT::Message[7] = taunt10;
$AT::Message[8] = tautn11;
$AT::Message[9] = taunt2;
$AT::Message[10] = noprob;
$AT::Message[11] = oops2;
$AT::Message[12] = dsgst5;
$AT::Message[13] = cheer2;
$AT::Message[14] = cheer1;
$AT::Message[15] = dsgst2;
$AT::Message[16] = taunt1;

// Total number of taunt messages above.
$AT::Count = 16;

// Spam Interval
$AT::SpamInterval = "0.1";

// Default Spam
$AT::CurrentSpam = 1;

// ==========================================================================

bind($AT::Key,"AT::Taunt();","", AutoTaunt);
bind("control s","AT::Spam(1);","AT::Spam(0);",TauntSpam);
bind("alt s","AT::Cycle();");


function AT::Taunt()
{
  %int = floor(getRandom()*$AT::Count)+1;
  localMessage($AT::Message[%int]);
}
Event::Attach(EventYouKilled, AT::Taunt);


//Hud::New(TauntSpamHud,0,"100%-56",200,28,"Current Taunt Spam:\n<F2>" @ $AT::Label[$AT::Message[$AT::CurrentSpam]]);
//function AT::Cycle()
//{
  //$AT::CurrentSpam++;
  //if ($AT::CurrentSpam > $AT::Count) { $AT::CurrentSpam = 1; }
  //Hud::Update(TauntSpamHud,"Current Taunt Spam:\n<F2>" @ $AT::Label[$AT::Message[$AT::CurrentSpam]]);
//}

//function AT::Spam(%state)
//{
  //if (%state==1)
  //{ 
    //localMessage($AT::Message[$AT::CurrentSpam]);
    //Interval::Start("localMessage(" @ $AT::Message[$AT::CurrentSpam] @ ");", $AT::SpamInterval, TAUNT_SPAM);
  //}
  //else
  //{
    //Interval::Cancel(TAUNT_SPAM);
  //}
//}

// The labels and their waves
$AT::Label[acknow] = "Acknowledged";
$AT::Label[cheer3] = "Alright!";
$AT::Label[dsgst4] = "Aarrg!";
$AT::Label[color7] = "Ah Crap!";
$AT::Label[bye] = "Bye";
$AT::Label[cease] = "Cease fire!";
$AT::Label[taunt4] = "Come get some";
$AT::Label[color6] = "Damnit";
$AT::Label[taunt3] = "Dance!";
$AT::Label[oops1] = "Doh";		
$AT::Label[dsgst1] = "Duh";
$AT::Label[help] = "Help!!";																			
$AT::Label[wshoot1] = "Heyy!!";
$AT::Label[hello] = "Hi";
$AT::Label[color3] = "Hmmm";
$AT::Label[taunt10] = "How'd that feel?";
$AT::Label[hurystn] = "Hurry up with the station!";
$AT::Label[dontkno] = "I dont know";										
$AT::Label[tautn11] = "I've had worse!";
$AT::Label[taunt2] = "Missed me!";
$AT::Label[no] = "No";
$AT::Label[noprob] = "No Problem";				
$AT::Label[oops2] = "Oops";
$AT::Label[ready] = "Ready";
$AT::Label[retreat] = "Retreat!";				
$AT::Label[color2] = "Shazbot!";
$AT::Label[sorry] = "Sorry";
$AT::Label[thanks] = "Thanks";						
$AT::Label[dsgst5] = "Uhhhh";
$AT::Label[wait1] = "Wait";
$AT::Label[wait2] = "Waiting";
$AT::Label[wshoot3] = "Watch where you're shooting";
$AT::Label[cheer2] = "Whoohoo";
$AT::Label[cheer1] = "Yeeah";
$AT::Label[yes] = "Yes";						
$AT::Label[dsgst2] = "You Idiot!";
$AT::Label[taunt1] = "Yoohoo";