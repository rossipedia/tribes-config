Event::Attach(EventGuiOpen,HUD::GuiOpen);
function HUD::NewFrame(%name,%x,%y,%w,%h,%gui)
{
  HUD::Destroy(%name);  
  if (%x=="" || %y=="" || %w=="" || %h=="")
  {
    echo("Missing coordinate for hud: "@%name);
    return;
  }
  %w = HUD::ParseCoord(w,%w,0,getScreenSize(x));
  %h = HUD::ParseCoord(h,%h,0,getScreenSize(y));

  %x = HUD::ParseCoord(x,%x,%w,getScreenSize(x));
  %y = HUD::ParseCoord(y,%y,%h,getScreenSize(y));

  if (%gui=="")
    %gui = PlayGui;

  $HUD::Count++;
  $HUD::[$HUD::Count] = $HUD::ID[%name] = newObject(%name,SimGui::Control,%x,%y,%w,%h);
  $HUD::Name[$HUD::ID[%name]] = %name;
  $HUD::Gui[%name] = %gui;

  if (ActivateGroup(%gui))
  {
    addToSet(%gui,$HUD::ID[%name]);
    $HUD::Attached[%name] = "TRUE";    
  }

  $HUD::CellCount[%name] = 0;

  HUD::AddObj(%name,%name@_BG,FearGui::FearGuiMenu,0,0,%w,%h);
  $HUD::Exists[%name] = "TRUE";
}

function HUD::Destroy(%hud)
{
  if (IsObject($HUD::ID[%hud]))
  {
    HUD::Remove(%hud);
    deleteObject($HUD::ID[%hud]);
    $HUD::[%hud] = "";
  }
}

function HUD::AddCell(%hud,%x,%y,%w,%h)
{
  if (%x=="") { %x=0; }
  if (%y=="") { %y=0; }
  if (%w=="") { %w=0; }
  if (%h=="") { %h=0; }
  
  %w = HUD::ParseCoord(w,%w,0,HUD::GetCoord(%hud,w));
  %h = HUD::ParseCoord(h,%h,0,HUD::GetCoord(%hud,h));
  %x = HUD::ParseCoord(x,%x,%w,HUD::GetCoord(%hud,w));
  %y = HUD::ParseCoord(y,%y,%h,HUD::GetCoord(%hud,h));


  $HUD::Cell[%hud,$HUD::CellCount[%hud]] = 
    newObject(%hud@"CELL"@$HUD::CellCount[%hud],FearGuiFormattedText,%x,%y,%w,%h);

  addToSet(
    $HUD::ID[%hud],
    $HUD::Cell[%hud,$HUD::CellCount[%hud]]
    );
  
  $HUD::CellCount[%hud]++;
  return $HUD::CellCount[%hud]-1;
}

function HUD::GetCellCount(%hud)
{
  return $HUD::CellCount[%hud];
}

function HUD::UpdateCell(%hud,%num,%value)
{
  Control::SetValue(%hud@"CELL"@%num,%value);
}

function HUD::ResizeCell(%hud,%num,%w,%h)
{
  if (%w=="") { %w=0; }
  if (%h=="") { %h=0; }
  %w = HUD::ParseCoord(w,%w,0,HUD::GetCoord(%hud,w));
  %h = HUD::ParseCoord(h,%h,0,HUD::GetCoord(%hud,h));
  Control::SetExtent(%hud@"CELL"@%num,%w,%h);
}

function HUD::MoveCell(%hud,%num,%x,%y)
{
  if (%x=="") { %x=0; }
  if (%y=="") { %y=0; }
  %dims = Control::GetExtent(%hud@"CELL"@%num);
  %w = getWord(%dims,0);
  %h = getWord(%dims,1);
  %x = HUD::ParseCoord(x,%x,%w,HUD::GetCoord(%hud,w));
  %y = HUD::ParseCoord(y,%y,%h,HUD::GetCoord(%hud,h));
  Control::SetPosition(%hud@"CELL"@%num,%x,%y);
}

function HUD::GetCellCoords(%hud,%num)
{
  return Control::GetPosition(%hud@"CELL"@%num) @ " " @ Control::GetExtent(%hud@"CELL"@%num);
}

function HUD::GetCellCoord(%hud,%num,%coord)
{
  return getWord(HUD::GetCellCoords(%hud,%num),$HUD::Coord[%coord]);
}

function HUD::MoveCellBy(%hud,%num,%coord,%by)
{
  HUD::SetCellCoord(%hud,%num,%coord,HUD::GetCellCoord(%hud,%num,%coord)+%by);
}

function HUD::SetCellCoord(%hud,%num,%coord,%value)
{
  if (%coord==x)
    HUD::MoveCell(%hud,%num,%value,HUD::GetCellCoord(%hud,%num,y));
  else if (%coord==y)
    HUD::MoveCell(%hud,%num,HUD::GetCellCoord(%hud,%num,x),%value);
  else if (%coord==w)
    HUD::ResizeCell(%hud,%num,%value,HUD::GetCellCoord(%hud,%num,h));
  else if (%coord==h)
    HUD::ResizeCell(%hud,%num,HUD::GetCellCoord(%hud,%num,w),%value);
  else
    echo("Invalid coordinate specified: "@%value);
}

function HUD::New(%name,%x,%y,%w,%h,%deftext,%gui)
{
  echo("Creating Basic Hud: "@%name);
  HUD::NewFrame(%name,%x,%y,%w,%h,%gui);  
  HUD::AddCell(%name,3,-2,1000,%h);
  if (%deftext!="")
    HUD::Update(%name,%deftext);
}

function HUD::GuiOpen(%gui)
{
  for (%i=1;%i<=$HUD::Count;%i++)
  {
    %name = $HUD::Name[$HUD::[%i]];
    if (!$HUD::Attached[%name])
    {
      echo("Attaching HUD \"" @ %name @ "\" to \"" @ $HUD::Gui[%name] @ "\"");
      HUD::SetGui(%name,$HUD::Gui[%name]);
      Event::Trigger(EventHudAttached::@%name,$HUD::Gui[%name]);
    }
  }
}

function HUD::SetGui(%hud,%gui)
{
  HUD::Remove(%hud);
  addToSet(%gui,%hud);
  $HUD::Gui[%hud] = %gui;
  $HUD::Attached[%hud] = "TRUE";
  Event::Trigger(EventHudAttached::@%name,$HUD::Gui[%name]);
}

function HUD::Remove(%hud)
{
  if ((%curgroup = getGroup($HUD::ID[%hud])) != -1)
    removeFromSet(%curgroup,$HUD::ID[%hud]);
  $HUD::Attached[%hud] = "FALSE";
  Event::Trigger(EventHudRemoved::@%name,%curgroup);
}

function HUD::GetGui(%hud)
{
  return Object::GetName(getGroup($HUD::ID[%hud]));
}

function HUD::AddObj(%hud,%name,%class,%x,%y,%w,%h)
{
  if (%x=="") { %x=0; }
  if (%y=="") { %y=0; }
  if (%w=="") { %w=0; }
  if (%h=="") { %h=0; }
  %w = HUD::ParseCoord(w,%w,0,HUD::GetCoord(%hud,w));
  %h = HUD::ParseCoord(h,%h,0,HUD::GetCoord(%hud,h));
  
  %x = HUD::ParseCoord(x,%x,%w,HUD::GetCoord(%hud,w));
  %y = HUD::ParseCoord(y,%y,%h,HUD::GetCoord(%hud,h));
  %objid = newObject(%name,%class,%x,%y,%w,%h);
  addToSet($HUD::ID[%hud],%objid);
}

function HUD::Update(%hud,%value)
{
  HUD::UpdateCell(%hud,0,%value);
}

function HUD::MakeTrans(%hud,%state)
{
  if (%state)
    Control::SetVisible(%hud@_BG,"FALSE");
  else
    Control::SetVisible(%hud@_BG,"TRUE");
}

function HUD::IsTrans(%hud)
{
  return !Control::GetVisible(%hud@_BG);
}

function HUD::SetVisible(%hud,%state)
{
  Control::SetVisible(%hud,%state);
}

function HUD::GetVisible(%hud)
{
  return Control::GetVisible(%hud);
}

function HUD::Resize(%hud,%w,%h)
{
  if (%w=="" || %h=="")
  {
    echo("Please provide a value to resize "@%hud@" to.");
    return;
  }
  if (%w < 0) %w = 0;
  if (%h < 0) %h = 0;
  if (%w > getScreenSize(x)) %w = getScreenSize(x);
  if (%h > getScreenSize(y)) %h = getScreenSize(y);
  if (HUD::GetCoord(%hud,x) + %w > getScreenSize(x))
    HUD::SetCoord(%hud,x,getScreenSize(x)-%w);
  if (HUD::GetCoord(%hud,y) + %h > getScreenSize(y))
    HUD::SetCoord(%hud,y,getScreenSize(y)-%h);
  Control::SetExtent(%hud,%w,%h);
  Control::SetExtent(%hud @ _BG,%w,%h);
  Event::Trigger(EventHudResized::@%hud,%w,%h);
}

function HUD::MoveTo(%hud,%x,%y)
{
  if (%x=="" || %y=="")
  {
    echo("Please provide a value to move "@%hud@" to.");
    return;
  }
  if (HUD::GetCoord(%hud,w) + %x > getScreenSize(x))
    %x = getScreenSize(x) - HUD::GetCoord(%hud,w);
  if (HUD::GetCoord(%hud,h) + %y > getScreenSize(y))
    %y = getScreenSize(y) - HUD::GetCoord(%hud,h);
  if (%x<0) %x = 0;
  if (%y<0) %y = 0;
  Control::SetPosition(%hud,%x,%y);
  Event::Trigger(EventHudMoved::@%hud,%x,%y);
}

function HUD::GetDimensions(%hud)
{
  return Control::GetPosition(%hud) @ " " @ Control::GetExtent(%hud);
}

$HUD::Coord[x] = 0;
$HUD::Coord[y] = 1;
$HUD::Coord[w] = 2;
$HUD::Coord[h] = 3;

$HUD::ScrSizeCoord[x] = x;
$HUD::ScrSizeCoord[y] = y;
$HUD::ScrSizeCoord[w] = x;
$HUD::ScrSizeCoord[h] = y;

function HUD::GetCoord(%hud,%coord)
{
  return getWord(HUD::GetDimensions(%hud),$HUD::Coord[%coord]);
}

function HUD::SetCoord(%hud,%coord,%value)
{
  %value = HUD::ParseCoord(%coord,%value,0,getScreenSize($HUD::ScrSizeCoord[%coord]));
  if (%coord == x)
    HUD::MoveTo(%hud,%value,getWord(Control::GetPosition(%hud),1));
  else if (%coord == y)
    HUD::MoveTo(%hud,getWord(Control::GetPosition(%hud),0),%value);
  else if (%coord == w)
    HUD::Resize(%hud,%value,getWord(Control::GetExtent(%hud),1));
  else if (%coord == h)
    HUD::Resize(%hud,getWord(Control::GetExtent(%hud),0),%value);
}

function HUD::ParseCoord(%coord,%value,%offset,%parent)
{
  if (String::Match(%value,"*+*"))
  {
    %l = String::MatchResult(0);
    %r = String::MatchResult(1);
    return HUD::ParseCoord(%coord,%l,0,%parent) + HUD::ParseCoord(%coord,%r,0,%parent);
  }
  else if (String::Match(%value,"*-*"))
  {
    %l = String::MatchResult(0);
    %r = String::MatchResult(1);
    return HUD::ParseCoord(%coord,%l,0,%parent) - HUD::ParseCoord(%coord,%r,0,%parent);
  }
  else if (String::Match(%value,"&*&","&"))
  {
    %l = String::MatchResult(0);
    %r = String::MatchResult(1);
    return HUD::ParseCoord(%coord,%l,0,%parent) * HUD::ParseCoord(%coord,%r,0,%parent);
  }
  else if (String::Match(%value,"*/*"))
  {
    %l = String::MatchResult(0);
    %r = String::MatchResult(1);
    return HUD::ParseCoord(%coord,%l,0,%parent) / HUD::ParseCoord(%coord,%r,0,%parent);
  }
  else if (String::Match(%value,"*%"))
  {
    return (%parent-%offset) * (String::MatchResult(0)/100);
  }
  else if (IsNum(%value))
  {
    return %value;
  }
  else
    echo("Incorrect value for HUD coordinate: "@%value);
}