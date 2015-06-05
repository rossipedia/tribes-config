editActionMap("playMap.sae");

bindCommand(mouse0, make, button0, TO,"Fire::OnPress();");
bindCommand(mouse0, break, button0, TO, "Fire::OnRelease();");

function Fire::OnPress()
{
  $Fire::Pressed = "TRUE";
  %wpn = getMountedItem(0);
  if (Event::Trigger(EventFirePressed,%wpn)!="FALSE")
    Event::Trigger(EventWeaponFired,%wpn);
}

function Fire::OnRelease()
{
  $Fire::Pressed = "";
  %wpn = getMountedItem(0);
  if (Event::Trigger(EventFireReleased,%wpn)!="FALSE")
    Event::Trigger(EventWeaponBreak,%wpn);
}

Event::Attach(EventWeaponFired,"postAction(2048, IDACTION_FIRE1, 1);");
Event::Attach(EventWeaponBreak,"postAction(2048, IDACTION_BREAK1, 1);");