bind("v","MS::Display(\"Chat Menu\");","",ChatMenu);

function Chat::AddGlobal(%menu,%key,%display,%text,%wav)
{
  if (%wav!="")
    %text = %text @ "~w" @ %wav;
  MS::AddChoice(%menu,%key,%display,"say(0,\""@%text@"\");");
}

function Chat::AddTeam(%menu,%key,%display,%text,%wav)
{
  if (%wav!="")
    %text = %text @ "~w" @ %wav;
  MS::AddChoice(%menu,%key,%display,"say(1,\""@%text@"\");");
}

function Chat::Local(%menu, %key, %display, %wav)
{
  MS::AddChoice(%menu, %key, %display, "localMessage("@%wav@");");
}

function Chat::Anim(%menu, %key, %display, %anim, %wav)
{
  MS::AddChoice(%menu, %key, %display, iif(%wav!="", "remoteEval(2048,playAnimWav,"@%anim@","@%wav@");", "remoteEval(2048,playAnimWav,"@%anim@");"));
}

function Chat::Command(%menu, %key, %display, %action, %text, %wav)
{
  if (%wav!="")
    %text = %text @ "~w" @ %wav;
  MS::AddChoice(%menu, %key, %display, "remoteEval(2048, CStatus, "@%action@","@%text@");");
}


MS::NewMenu("Chat Menu");

MS::NewMenu("Animations");
MS::AddMenu("Chat Menu","a","Animations");

Chat::Anim("Animations","l","Alright!", 7, cheer3);
Chat::Anim("Animations","a","Arg!", 3, dsgst4);
Chat::Anim("Animations","b","Bye", 12, bye);
Chat::Anim("Animations","c","Come get some", 9, taunt4);
Chat::Anim("Animations","h","Hi", 12, hello);
Chat::Anim("Animations","d","How'd that feel?", 8, taunt10);
Chat::Anim("Animations","m","Move out of way", 1, outway);
Chat::Anim("Animations","o","Over here", 0, ovrhere);
Chat::Anim("Animations","s","Pose: Stand", 11);
Chat::Anim("Animations","k","Pose: Kneel", 10);
Chat::Anim("Animations","w","Whoohoo", 6, cheer2);
Chat::Anim("Animations","e","Yeaah!", 5, cheer1);					
Chat::Anim("Animations","r","Retreat", 2, retreat);
Chat::Anim("Animations","y","Yes - Salute", 4, yes);


MS::NewMenu("Command Response");
MS::AddMenu("Chat Menu","c","Command Response");

Chat::Command("Command Response","a","Acknowledged", 1, "Command acknowledged", "acknow");
Chat::Command("Command Response","u","Unable to complete", 0, "Unable to complete objective", "objxcmp");
Chat::Command("Command Response","o","Objective completed", 0, "Objective complete", "objcomp");


MS::NewMenu("Defense");
MS::AddMenu("Chat Menu","d","Defense");

Chat::AddTeam("Defense","b","Defend our base", "Defend our base", defbase);
Chat::AddTeam("Defense","h","Need Heavy on Flag!!!", "Need Heavy on Flag!!!", defbase);
Chat::AddTeam("Defense","d","Defending our base", "Defending our base", defend);
Chat::AddTeam("Defense","g","Go on the defensive!", "Go on the defensive!", godef);
Chat::AddTeam("Defense","i","Incoming enemies", "Incoming enemies!", incom2);
Chat::AddTeam("Defense","c","Is base clear?", "Is our base clear?", isbsclr);
Chat::AddTeam("Defense","t","Our base has been Taken", "Our Base has been taken", basetkn);		 										
Chat::AddTeam("Defense","s","Our base is secure", "Our Base is secure", bsclr2);
Chat::AddTeam("Defense","n","We need more defense", "We need more defense", needdef);							


MS::NewMenu("Flag");
MS::AddMenu("Chat Menu","f","Flag");

Chat::AddTeam("Flag","c","Clear the mines from our flag", "Clear the mines from our flag", clrflg);
Chat::AddTeam("Flag","f","Get the enemy flag", "Get the enemy flag", geteflg);
Chat::AddTeam("Flag","i","I have the enemy flag", "I have the enemy flag. Heading back to our base", haveflg);	
Chat::AddTeam("Flag","h","Mines have been cleared", "Mines have been cleared", mineclr);
Chat::AddTeam("Flag","m","Mine the flag!", "Mine the flag", mineflg);
Chat::AddTeam("Flag","s","Our flag is secure", "Our flag is secure", flaghm);			
Chat::AddTeam("Flag","o","Our flag is mined", "Our flag is mined", flgmine);
Chat::AddTeam("Flag","r","Return our flag to our base", "Return our flag to our base", retflag);			
Chat::AddTeam("Flag","g","I am going for the enemy flag","I am going for the enemy flag", ono);
Chat::AddTeam("Flag","v","I have the enemy flag, Cover Me!!!", "I have the enemy flag, Cover Me!!!",  coverme);
Chat::AddTeam("Flag","d","I have the enemy flag, return our flag to our base!", "I have the enemy flag, return our flag to our base!", retflag);

MS::NewMenu("Instructions");
MS::AddMenu("Chat Menu","i","Instructions");

Chat::AddTeam("Instructions","l","Belay order", "Belay order", belay);
Chat::AddTeam("Instructions","b","Board APC!", "Board APC", boarda);
Chat::AddTeam("Instructions","m","Move out!", "Move out!", moveout);
Chat::AddTeam("Instructions","o","Order canceled", "Order canceled", ordcan);
Chat::AddTeam("Instructions","p","Proceed ahead", "Proceed ahead", proceed);		
Chat::AddTeam("Instructions","r","Regroup", "Regroup", regroup);		

MS::NewMenu("Global");
MS::AddMenu("Chat Menu","g","Global");

Chat::AddGlobal("Global","a","Acknowledged", "Acknowledged", acknow);
Chat::AddGlobal("Global","1","Alright!", "Alright!", cheer3);
Chat::AddGlobal("Global","2","Aarrg!", "Aarrg!", dsgst4);
Chat::AddGlobal("Global","3","Ah Crap!", "Ah Crap!", color7);
Chat::AddGlobal("Global","b","Bye", "Bye!", bye);
Chat::AddGlobal("Global","c","Cease fire!", "Cease fire!", cease);
Chat::AddGlobal("Global","4","Come get some", "Come get some", taunt4);
Chat::AddGlobal("Global","5","Damnit", "Damnit!", color6);
Chat::AddGlobal("Global","6","Dance!", "Dance!", taunt3);
Chat::AddGlobal("Global","7","Doh", "Doh!", oops1);		
Chat::AddGlobal("Global","8","Duh", "Duh", dsgst1);
Chat::AddGlobal("Global","e","Help!!", "Help!!", help);									
Chat::AddGlobal("Global","9","Heyy!!", "Heyy!", wshoot1);
Chat::AddGlobal("Global","h","Hi", "Hi", hello);
Chat::AddGlobal("Global","m","Hmmm", "Hmmm!", color3);
Chat::AddGlobal("Global","0","How'd that feel?", "How'd that feel?", taunt10);
Chat::AddGlobal("Global","f","Hurry up with the station!", "Hurry up with the station!", hurystn);
Chat::AddGlobal("Global","i","I dont know", "I don't know", dontkno);		
Chat::AddGlobal("Global","[","I've had worse!", "I've had worse!", tautn11);
Chat::AddGlobal("Global","]","Missed me!", "Missed me!", taunt2);
Chat::AddGlobal("Global","n","No", "No", no);
Chat::AddGlobal("Global","p","No Problem", "No Problem", noprob);				
Chat::AddGlobal("Global","o","Oops", "Oops!", oops2);
Chat::AddGlobal("Global","r","Ready", "Ready", ready);
Chat::AddGlobal("Global","j","Retreat!", "Retreat!", retreat);				
Chat::AddGlobal("Global",";","Shazbot!", "Shazbot!", color2);
Chat::AddGlobal("Global","s","Sorry", "Sorry", sorry);
Chat::AddGlobal("Global","t","Thanks", "Thanks", thanks);						
Chat::AddGlobal("Global","u","Uhhhh", "Uhhhh!", dsgst5);
Chat::AddGlobal("Global","z","Wait", "Wait", wait1);
Chat::AddGlobal("Global","x","Waiting", "Waiting", wait2);
Chat::AddGlobal("Global","w","Watch where you're shooting", "Watch where your shooting", wshoot3);
Chat::AddGlobal("Global","'","Whoohoo", "Whoohoo", cheer2);
Chat::AddGlobal("Global",",","Yeeah", "Yeeah", cheer1);
Chat::AddGlobal("Global","y","Yes", "Yes", yes);						
Chat::AddGlobal("Global",".","You Idiot!", "You Idiot!", dsgst2);
Chat::AddGlobal("Global","/","Yoohoo", "Yoohoo", taunt1);


MS::NewMenu("Local");
MS::AddMenu("Chat Menu","l","Local");

Chat::Local("Local","a","Acknowledged", acknow);
Chat::Local("Local","1","Alright!", cheer3);
Chat::Local("Local","2","Aarrg!", dsgst4);
Chat::Local("Local","3","Ah Crap!", color7);
Chat::Local("Local","b","Bye", bye);
Chat::Local("Local","c","Cease fire!", cease);
Chat::Local("Local","4","Come get some", taunt4);
Chat::Local("Local","5","Damnit", color6);
Chat::Local("Local","6","Dance!", taunt3);
Chat::Local("Local","7","Doh", oops1);		
Chat::Local("Local","8","Duh", dsgst1);
Chat::Local("Local","e","Help!!", help);																			
Chat::Local("Local","9","Heyy!!", wshoot1);
Chat::Local("Local","h","Hi", hello);
Chat::Local("Local","m","Hmmm", color3);
Chat::Local("Local","0","How'd that feel?", taunt10);
Chat::Local("Local","f","Hurry up with the station!", hurystn);
Chat::Local("Local","i","I dont know", dontkno);										
Chat::Local("Local","[","I've had worse!", tautn11);
Chat::Local("Local","]","Missed me!", taunt2);
Chat::Local("Local","n","No", no);
Chat::Local("Local","p","No Problem", noprob);				
Chat::Local("Local","o","Oops", oops2);
Chat::Local("Local","r","Ready", ready);
Chat::Local("Local","j","Retreat!", retreat);				
Chat::Local("Local",";","Shazbot!", color2);
Chat::Local("Local","s","Sorry", sorry);
Chat::Local("Local","t","Thanks", thanks);						
Chat::Local("Local","u","Uhhhh", dsgst5);
Chat::Local("Local","z","Wait", wait1);
Chat::Local("Local","x","Waiting", wait2);
Chat::Local("Local","w","Watch where you're shooting", wshoot3);
Chat::Local("Local","'","Whoohoo", cheer2);
Chat::Local("Local",",","Yeeah", cheer1);
Chat::Local("Local","y","Yes", yes);						
Chat::Local("Local",".","You Idiot!", dsgst2);
Chat::Local("Local","/","Yoohoo", taunt1);


MS::NewMenu("Need");
MS::AddMenu("Chat Menu","n","Need");

Chat::AddTeam("Need","a","Can anyone bring me some ammo?", "Can anyone bring me some ammo?", needamo);
Chat::AddTeam("Need","p","I need an APC pickup", "I need an APC pickup", needpku);
Chat::AddTeam("Need","e","I need an escort back to base", "I need an escort back to base", needesc);
Chat::AddTeam("Need","r","Need Repairs", "Need repairs", needrep);

MS::NewMenu("Repair");
MS::AddMenu("Chat Menu","e","Repair");

Chat::AddTeam("Repair","g","Our Generator needs Repairs!", "Our Generator needs Repairs!", needrep);
Chat::AddTeam("Repair","s","Our Inventory Stations need Repairs!", "Our Inventory Stations need Repairs!", needrep);
Chat::AddTeam("Repair","t","Our Rocket Turret needs Repairs!", "Our Rocket Turret needs Repairs!", needrep);
Chat::AddTeam("Repair","p","Our Turrets need Repairs!", "Our Turrets need Repairs!", needrep);
Chat::AddTeam("Repair","m","Can a guy get some repairs over here?!", "Can a guy get some repairs over here?!", needrep);

MS::NewMenu("Offense");
MS::AddMenu("Chat Menu","o","Offense");

Chat::AddTeam("Offense","a","APC ready to go", "APC ready to go... waiting for passengers", waitpas);		
Chat::AddTeam("Offense","1","Attack!", "Attack!!!", attac2);
Chat::AddTeam("Offense","2","Attack!", "Attack!", attack);				
Chat::AddTeam("Offense","3","Attack the enemy base!", "Attack the enemy base!", attbase);
Chat::AddTeam("Offense","4","Attack the enemy!", "Attack the enemy!", attenem);
Chat::AddTeam("Offense","5","Attack objective!", "Attack objective!", attobj);
Chat::AddTeam("Offense","c","Capture objective!", "Capture objective!", capobj);
Chat::AddTeam("Offense","o","Get objective!", "Get objective!", getobj);								
Chat::AddTeam("Offense","t","Go on the offensive!", "Go on the offensive!", gooff);
Chat::AddTeam("Offense","g","Going Offense!","Going Offense!", ono);
Chat::AddTeam("Offense","w","Wait for my signal to attack", "Wait for my signal to attack", waitsig);

MS::NewMenu("Target");
MS::AddMenu("Chat Menu","r","Target");

Chat::AddTeam("Target","t","Destroy enemy turret", "Destroy enemy turret", destur);
Chat::AddTeam("Target","g","Destroy the enemy generator", "Destroy the enemy generator", desgen);
Chat::AddTeam("Target","d","Enemy generator destroyed", "Enemy generator destroyed", gendes);
Chat::AddTeam("Target","e","Enemy turret destroyed", "Enemy turret destroyed", turdes);
Chat::AddTeam("Target","f","Fire on my target", "Fire on my target", firetgt);
Chat::AddTeam("Target","i","I need a target", "I need a target", needtgt);						
Chat::AddTeam("Target","a","Target acquired", "Target acquired", tgtacq);		
Chat::AddTeam("Target","l","Target location", "Target location", tgtobj);
Chat::AddTeam("Target","o","Target out of range", "Target out of range", tgtout);

MS::NewMenu("Team");
MS::AddMenu("Chat Menu","t","Team");

Chat::AddTeam("Team","a","Acknowledged", "Acknowledged", acknow);
Chat::AddTeam("Team","1","Alright!", "Alright!", cheer3);
Chat::AddTeam("Team","2","Aarrg!", "Aarrg!", dsgst4);
Chat::AddTeam("Team","3","Ah Crap!", "Ah Crap!", color7);
Chat::AddTeam("Team","b","Bye", "Bye!", bye);
Chat::AddTeam("Team","c","Cease fire!", "Cease fire!", cease);
Chat::AddTeam("Team","v","Cover me!","Cover Me!", coverme);
Chat::AddTeam("Team","4","Come get some", "Come get some", taunt4);
Chat::AddTeam("Team","5","Damnit", "Damnit!", color6);
Chat::AddTeam("Team","6","Dance!", "Dance!", taunt3);
Chat::AddTeam("Team","7","Doh", "Doh!", oops1);		
Chat::AddTeam("Team","8","Duh", "Duh", dsgst1);
Chat::AddTeam("Team","e","Help!!", "Help!!", help);									
Chat::AddTeam("Team","9","Heyy!!", "Heyy!", wshoot1);
Chat::AddTeam("Team","h","Hi", "Hi", hello);
Chat::AddTeam("Team","m","Hmmm", "Hmmm!", color3);
Chat::AddTeam("Team","0","How'd that feel?", "How'd that feel?", taunt10);
Chat::AddTeam("Team","f","Hurry up with the station!", "Hurry up with the station!", hurystn);
Chat::AddTeam("Team","i","I dont know", "I don't know", dontkno);			
Chat::AddTeam("Team","[","I've had worse!", "I've had worse!", tautn11);
Chat::AddTeam("Team","]","Missed me!", "Missed me!", taunt2);
Chat::AddTeam("Team","n","No", "No", no);
Chat::AddTeam("Team","p","No Problem", "No Problem", noprob);				
Chat::AddTeam("Team","o","Oops", "Oops!", oops2);
Chat::AddTeam("Team","r","Ready", "Ready", ready);
Chat::AddTeam("Team","j","Retreat!", "Retreat!", retreat);				
Chat::AddTeam("Team",";","Shazbot!", "Shazbot!", color2);
Chat::AddTeam("Team","s","Sorry", "Sorry", sorry);
Chat::AddTeam("Team","t","Thanks", "Thanks", thanks);						
Chat::AddTeam("Team","u","Uhhhh", "Uhhhh!", dsgst5);
Chat::AddTeam("Team","z","Wait", "Wait", wait1);
Chat::AddTeam("Team","x","Waiting", "Waiting", wait2);
Chat::AddTeam("Team","w","Watch where you're shooting", "Watch where your shooting", wshoot3);
Chat::AddTeam("Team","'","Whoohoo", "Whoohoo", cheer2);
Chat::AddTeam("Team",",","Yeeah", "Yeeah", cheer1);
Chat::AddTeam("Team","y","Yes", "Yes", yes);						
Chat::AddTeam("Team",".","You Idiot!", "You Idiot!", dsgst2);
Chat::AddTeam("Team","/","Yoohoo", "Yoohoo", taunt1);

MS::NewMenu("Taunt");
MS::AddMenu("Chat Menu","v","Taunt");

Chat::AddGlobal("Taunt","c","CRAP!","Sheepswallop!! Sheepswallop and bloody buttered onions!!",color7);
Chat::AddGlobal("Taunt","z","OWW!","MOMMY IT HURTS!!",death);
Chat::AddGlobal("Taunt","b","Lord!","I AM THE LORD!!",taunt4);
Chat::AddGlobal("Taunt","a","base","All Your Base Are Belong To Us!!!",taunt4);
Chat::AddGlobal("Taunt","s","Silly","Silly faggot! Dicks are for chicks!",dsgst1);