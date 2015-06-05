// Scripts for the Nostromo game pad


// buttons
// 0: forward
// 1: back
// 2: left
// 3: right
// 4: up-left
// 5: up-right
// 6: back-left
// 7: back-right

//bindAction(joystick1, make, button0, TO, IDACTION_MOVEFORWARD, 1.000000);
//bindAction(joystick1, break, button0, TO, IDACTION_MOVEFORWARD, 0.000000);
//bindAction(joystick1, make, button1, TO, IDACTION_MOVERIGHT, 1.000000);
//bindAction(joystick1, break, button1, TO, IDACTION_MOVERIGHT, 0.000000);
//bindAction(joystick1, make, button2, TO, IDACTION_MOVEBACK, 1.000000);
//bindAction(joystick1, break, button2, TO, IDACTION_MOVEBACK, 0.000000);
//bindAction(joystick1, make, button3, TO, IDACTION_MOVELEFT, 1.000000);
//bindAction(joystick1, break, button3, TO, IDACTION_MOVELEFT, 0.000000);

// Drop Backpack key
//bind("button15","drop(Backpack);","","","","joystick1");

// Bypass scavenge key
bind("]","Toggle::Var(\"$pref::Favs::Scavenge\",\"Scavenging\");");
bind("[","Favs::Scavenge();");
bind(".","drop(Flag);");
bind("/","drop(Backpack);");
bind("rshift","TPU::Show();","TPU::Hide();");