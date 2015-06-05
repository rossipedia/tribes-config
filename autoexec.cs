exec("Support\\Support.cs");
Exclude("QuickTurn");

inputActivate(all);

LoadFolder("support");
LoadFolder("scripts");

bind("control end","quit();");
bind("control home", "endGame();");
bind("button3","throw(\"Mine\",1000);");
bind("button4","throw(\"Grenade\",1000);");
bind("q","throw(\"Mine\",1000);");
bind("f","throw(\"Grenade\",1000);");
