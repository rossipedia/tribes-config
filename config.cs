NewActionMap("actionMap.sae");
bindAction(keyboard0, make, "tab", TO, IDACTION_MENU_PAGE, 1.000000);
bindAction(keyboard0, make, "escape", TO, IDACTION_ESCAPE_PRESSED, 0.000000);
bindAction(keyboard0, make, "k", TO, IDACTION_MENU_PAGE, 2.000000);
bindAction(keyboard0, make, "t", TO, IDACTION_CHAT, 0.000000);
bindAction(keyboard0, make, "y", TO, IDACTION_CHAT, 1.000000);
bindAction(keyboard0, make, "u", TO, IDACTION_CHAT_DISP_SIZE, -1.000000);
bindAction(keyboard0, make, "prior", TO, IDACTION_CHAT_DISP_PAGE, -1.000000);
bindAction(keyboard0, make, "next", TO, IDACTION_CHAT_DISP_PAGE, 1.000000);
bindCommand(keyboard0, make, "f1", TO, "remoteEval(2048, PlayMode);");
bindCommand(keyboard0, make, "o", TO, "remoteEval(2048, ToggleObjectivesMode);");
bindCommand(keyboard0, make, "i", TO, "remoteEval(2048, ToggleInventoryMode);");
bindCommand(keyboard0, make, "c", TO, "remoteEval(2048, ToggleCommandMode);");
bindCommand(keyboard0, make, control, "x", TO, "commandAck();");
bindCommand(keyboard0, make, control, "d", TO, "commandDeclined();");
bindCommand(keyboard0, make, control, "c", TO, "commandCompleted();");
bindCommand(keyboard0, make, control, "y", TO, "voteYes();");
bindCommand(keyboard0, make, control, "n", TO, "voteNo();");
bindCommand(keyboard0, make, control, "e", TO, "targetClient();");
NewActionMap("playMap.sae");
bindAction(keyboard0, make, "a", TO, IDACTION_MOVELEFT, 1.000000);
bindAction(keyboard0, break, "a", TO, IDACTION_MOVELEFT, 0.000000);
bindAction(keyboard0, make, "d", TO, IDACTION_MOVERIGHT, 1.000000);
bindAction(keyboard0, break, "d", TO, IDACTION_MOVERIGHT, 0.000000);
bindAction(keyboard0, make, "s", TO, IDACTION_MOVEBACK, 1.000000);
bindAction(keyboard0, break, "s", TO, IDACTION_MOVEBACK, 0.000000);
bindAction(keyboard0, make, "w", TO, IDACTION_MOVEFORWARD, 1.000000);
bindAction(keyboard0, break, "w", TO, IDACTION_MOVEFORWARD, 0.000000);
bindAction(keyboard0, make, "r", TO, IDACTION_VIEW);
bindCommand(keyboard0, make, "b", TO, "use(\"Beacon\");");
bindCommand(keyboard0, make, "m", TO, "throwStart();");
bindCommand(keyboard0, break, "m", TO, "throwRelease(\"Mine\");");
bindCommand(keyboard0, make, "6", TO, "use(\"Laser Rifle\");");
bindCommand(keyboard0, make, "7", TO, "use(\"ELF gun\");");
bindCommand(keyboard0, make, "8", TO, "use(\"Mortar\");");
bindCommand(keyboard0, make, "9", TO, "use(\"Targeting Laser\");");
bindAction(keyboard0, make, "numpad8", TO, IDACTION_LOOKUP, 0.099990);
bindAction(keyboard0, break, "numpad8", TO, IDACTION_LOOKUP, 0.000000);
bindAction(keyboard0, make, "numpad2", TO, IDACTION_LOOKDOWN, 0.099990);
bindAction(keyboard0, break, "numpad2", TO, IDACTION_LOOKDOWN, 0.000000);
bindAction(keyboard0, make, "numpad6", TO, IDACTION_TURNRIGHT, 0.099990);
bindAction(keyboard0, break, "numpad6", TO, IDACTION_TURNRIGHT, 0.000000);
bindAction(keyboard0, make, "numpad4", TO, IDACTION_TURNLEFT, 0.099990);
bindAction(keyboard0, break, "numpad4", TO, IDACTION_TURNLEFT, 0.000000);
bindAction(keyboard0, make, "numpad5", TO, IDACTION_CENTERVIEW);
bindCommand(keyboard0, make, control, "w", TO, "drop(Weapon);");
bindCommand(keyboard0, make, control, "a", TO, "drop(Ammo);");
bindCommand(keyboard0, make, control, "f", TO, "drop(Flag);");
bindCommand(keyboard0, make, control, "k", TO, "kill();");
bindCommand(keyboard0, make, shift, "q", TO, "prevWeapon();");
bindAction(mouse0, xaxis0, TO, IDACTION_YAW, Flip, Scale, 0.001381);
bindAction(mouse0, yaxis0, TO, IDACTION_PITCH, Flip, Scale, 0.001381);
bindCommand(keyboard0, make, "capslock", TO, "use(\"Backpack\");");
bindCommand(keyboard0, make, control, "capslock", TO, "drop(Backpack);");
bindCommand(mouse0, make, button2, TO, "use(\"Repair Kit\");");
bindCommand(mouse0, make, button0, TO, "Fire::OnPress();");
bindCommand(mouse0, break, button0, TO, "Fire::OnRelease();");
bindCommand(keyboard0, make, "e", TO, "Zoom::Pressed();");
bindCommand(keyboard0, break, "e", TO, "Zoom::Released();");
bindCommand(keyboard0, make, "x", TO, "Zoom::Inc();");
bindCommand(keyboard0, break, "x", TO, "null();");
bindCommand(keyboard0, make, "z", TO, "Zoom::Dec();");
bindCommand(keyboard0, break, "z", TO, "null();");
bindCommand(keyboard0, make, "4", TO, "WK::Use(4);");
bindCommand(keyboard0, break, "4", TO, "null();");
bindCommand(keyboard0, make, "5", TO, "WK::Use(5);");
bindCommand(keyboard0, break, "5", TO, "null();");
bindCommand(keyboard0, make, "3", TO, "WK::Use(3);");
bindCommand(keyboard0, break, "3", TO, "null();");
bindCommand(keyboard0, make, "2", TO, "WK::Use(2);");
bindCommand(keyboard0, break, "2", TO, "null();");
bindCommand(keyboard0, make, "1", TO, "WK::Use(1);");
bindCommand(keyboard0, break, "1", TO, "null();");
bindCommand(keyboard0, make, control, "q", TO, "TPU::Show();");
bindCommand(keyboard0, break, control, "q", TO, "TPU::Hide();");
bindCommand(keyboard0, make, "n", TO, "AT::Taunt();");
bindCommand(keyboard0, break, "n", TO, "null();");
bindCommand(keyboard0, make, control, "s", TO, "AT::Spam(1);");
bindCommand(keyboard0, break, control, "s", TO, "AT::Spam(0);");
bindCommand(keyboard0, make, alt, "s", TO, "AT::Cycle();");
bindCommand(keyboard0, break, alt, "s", TO, "null();");
bindCommand(keyboard0, make, alt, "t", TO, "Target::Menu();");
bindCommand(keyboard0, break, alt, "t", TO, "null();");
bindCommand(keyboard0, make, "lshift", TO, "QuickLR::On();");
bindCommand(keyboard0, break, "lshift", TO, "QuickLR::Off();");
bindCommand(keyboard0, make, "]", TO, "Toggle::Var(\"$pref::Favs::Scavenge\",\"Scavenging\");");
bindCommand(keyboard0, break, "]", TO, "null();");
bindCommand(keyboard0, make, "[", TO, "Favs::Scavenge();");
bindCommand(keyboard0, break, "[", TO, "null();");
bindCommand(keyboard0, make, "period", TO, "drop(Flag);");
bindCommand(keyboard0, break, "period", TO, "null();");
bindCommand(keyboard0, make, "/", TO, "drop(Backpack);");
bindCommand(keyboard0, break, "/", TO, "null();");
bindCommand(keyboard0, make, "rshift", TO, "TPU::Show();");
bindCommand(keyboard0, break, "rshift", TO, "TPU::Hide();");
bindCommand(keyboard0, make, control, "h", TO, "MH::CreateMenu();");
bindCommand(keyboard0, break, control, "h", TO, "null();");
bindCommand(keyboard0, make, "backspace", TO, "MDH::Toggle();");
bindCommand(keyboard0, break, "backspace", TO, "null();");
bindCommand(keyboard0, make, "space", TO, "Ski( 1 );");
bindCommand(keyboard0, break, "space", TO, "Ski( 0 );");
bindCommand(keyboard0, make, control, "numpadenter", TO, "Toggle::Var(\"$JJ::Active\",\"Jump Jet\");");
bindCommand(keyboard0, break, control, "numpadenter", TO, "null();");
bindCommand(mouse0, make, button1, TO, "JJ::Start();");
bindCommand(mouse0, break, button1, TO, "JJ::Stop();");
bindCommand(keyboard0, make, "numpadenter", TO, "Incoming::Trigger();");
bindCommand(keyboard0, break, "numpadenter", TO, "null();");
bindCommand(keyboard0, make, alt, "1", TO, "Favs::Select(1);");
bindCommand(keyboard0, break, alt, "1", TO, "null();");
bindCommand(keyboard0, make, alt, "2", TO, "Favs::Select(2);");
bindCommand(keyboard0, break, alt, "2", TO, "null();");
bindCommand(keyboard0, make, alt, "3", TO, "Favs::Select(3);");
bindCommand(keyboard0, break, alt, "3", TO, "null();");
bindCommand(keyboard0, make, alt, "4", TO, "Favs::Select(4);");
bindCommand(keyboard0, break, alt, "4", TO, "null();");
bindCommand(keyboard0, make, alt, "5", TO, "Favs::Select(5);");
bindCommand(keyboard0, break, alt, "5", TO, "null();");
bindCommand(keyboard0, make, alt, "6", TO, "Favs::Select(6);");
bindCommand(keyboard0, break, alt, "6", TO, "null();");
bindCommand(keyboard0, make, alt, "7", TO, "Favs::Select(7);");
bindCommand(keyboard0, break, alt, "7", TO, "null();");
bindCommand(keyboard0, make, alt, "g", TO, "Favs::Scavenge();");
bindCommand(keyboard0, break, alt, "g", TO, "null();");
bindCommand(keyboard0, make, "g", TO, "Favs::ScavengeBypass();");
bindCommand(keyboard0, break, "g", TO, "Favs::ScavengeBypassOff();");
bindCommand(keyboard0, make, control, "g", TO, "Toggle::Var(\"$pref::Favs::Scavenge\",\"Scavenging\");");
bindCommand(keyboard0, break, control, "g", TO, "null();");
bindCommand(keyboard0, make, alt, "d", TO, "DM::Show();");
bindCommand(keyboard0, break, alt, "d", TO, "null();");
bindCommand(keyboard0, make, alt, "home", TO, "DD::Start();");
bindCommand(keyboard0, break, alt, "home", TO, "null();");
bindCommand(keyboard0, make, alt, "end", TO, "DD::Stop();");
bindCommand(keyboard0, break, alt, "end", TO, "null();");
bindCommand(keyboard0, make, control, "d", TO, "daeHUD::ToggleHud();");
bindCommand(keyboard0, break, control, "d", TO, "null();");
bindCommand(keyboard0, make, control, "r", TO, "daeHUD::reset();");
bindCommand(keyboard0, break, control, "r", TO, "null();");
bindCommand(keyboard0, make, control, "numpad+", TO, "daeHUD::AddScore(\"Friendly\",1);");
bindCommand(keyboard0, break, control, "numpad+", TO, "null();");
bindCommand(keyboard0, make, control, "numpad-", TO, "daeHUD::AddScore(\"Friendly\",-1);");
bindCommand(keyboard0, break, control, "numpad-", TO, "null();");
bindCommand(keyboard0, make, alt, "numpad+", TO, "daeHUD::AddScore(\"Enemy\",1);");
bindCommand(keyboard0, break, alt, "numpad+", TO, "null();");
bindCommand(keyboard0, make, alt, "numpad-", TO, "daeHUD::AddScore(\"Enemy\",-1);");
bindCommand(keyboard0, break, alt, "numpad-", TO, "null();");
bindCommand(keyboard0, make, control, "t", TO, "daeHUD::ToggleTrans();");
bindCommand(keyboard0, break, control, "t", TO, "null();");
bindCommand(keyboard0, make, alt, "f", TO, "daeHUD::ShowCarrierMenu();");
bindCommand(keyboard0, break, alt, "f", TO, "null();");
bindCommand(keyboard0, make, "v", TO, "MS::Display(\"Chat Menu\");");
bindCommand(keyboard0, break, "v", TO, "null();");
bindCommand(keyboard0, make, control, "end", TO, "quit();");
bindCommand(keyboard0, break, control, "end", TO, "null();");
bindCommand(keyboard0, make, control, "home", TO, "endGame();");
bindCommand(keyboard0, break, control, "home", TO, "null();");
bindCommand(mouse0, make, button3, TO, "throw(\"Mine\",1000);");
bindCommand(mouse0, break, button3, TO, "null();");
bindCommand(mouse0, make, button4, TO, "throw(\"Grenade\",1000);");
bindCommand(mouse0, break, button4, TO, "null();");
bindCommand(keyboard0, make, "q", TO, "throw(\"Mine\",1000);");
bindCommand(keyboard0, break, "q", TO, "null();");
bindCommand(keyboard0, make, "f", TO, "throw(\"Grenade\",1000);");
bindCommand(keyboard0, break, "f", TO, "null();");
NewActionMap("pdaMap.sae");
bindAction(keyboard0, make, "z", TO, IDACTION_ZOOM_MODE_ON);
bindAction(keyboard0, break, "z", TO, IDACTION_ZOOM_MODE_OFF);
