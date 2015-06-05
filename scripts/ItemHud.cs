// ==========================================================================
$ItemHUD::Left = "160";
$ItemHUD::Top = "0";

// Items
$ItemHUD::Item[0] = "Mine";
$ItemHUD::Icon[0] = "icons\\Mine_Icon.bmp";

$ItemHUD::Item[1] = "Grenade";
$ItemHUD::Icon[1] = "icons\\Grenade_Icon.bmp";

$ItemHUD::Item[2] = "Beacon";
$ItemHUD::Icon[2] = "icons\\Beacon_Icon.bmp";

$ItemHUD::Item[3] = "Repair Kit";
$ItemHUD::Icon[3] = "icons\\RKit_Icon.bmp";

// ==========================================================================

Event::Attach(EventConnected, "ItemHUD::Update();");
Event::Attach(EventSpawn, "ItemHUD::Update();");
Event::Attach(EventRespawn, "ItemHUD::Update();");
Event::Attach(EventUseItem, "ItemHUD::Update();");
Event::Attach(EventThrowItem, "ItemHUD::Update();");
Event::Attach(EventDropItem, "ItemHUD::Update();");
Event::Attach(EventDeployItem, "ItemHUD::Update();");
Event::Attach(EventBuyItem, "ItemHUD::Update();");
Event::Attach(EventStationOff, "ItemHUD::Update();");
Event::Attach(EventSellItem, "ItemHUD::Update();");
Event::Attach(EventResupplied, "ItemHUD::Update();");

for (%i=0;$ItemHUD::Item[%i]!="";%i++) { $ItemHUD::NumItems = %i+1; }

HUD::New(ItemHUD,$ItemHUD::Left, $ItemHUD::Top, 38, $ItemHUD::NumItems * 14);

function ItemHUD::Update(%cancel) 
{ 
  for (%i=0;(%item = $ItemHUD::Item[%i])!="";%i++)
  {
    if (!%i)
      %text = "<B0,4:"@$ItemHUD::Icon[%i]@"><F2><L4>"@getItemCount(%item)@"\n";
    else
      %text = %text @ "<B-20,4:"@$ItemHUD::Icon[%i]@"><F2><L4>"@getItemCount(%item)@"\n";
  }
  HUD::Update(ItemHUD,%text);
  if (%cancel=="")
  {
    for (%i=0;%i<=5;%i++)
      schedule("ItemHUD::Update(1);",0.05*%i);
  }
}