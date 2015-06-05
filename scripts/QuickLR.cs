// ==========================================================================
// A tiny little script that basically uses one key to ready the sniping
// weapon of choice (on press), and then restore the previous weapon (on release)

// Set this to the key you want to use. Set it to the same key
// as your zoom key to ready the sniping weapon when you zoom in.
$QuickLR::Key = "lshift";

// Set this to "TRUE" to ready the sniping weapon when you zoom, 
// regardless of what key you normally use
$QuickLR::AttachToZoom = "FALSE";
// 
$QuickLR::Weapon = "Laser Rifle";
// ==========================================================================


if ($QuickLR::AttachToZoom || $QuickLR::Key == $Zoom::Key)
{
  Event::Attach(EventZoomIn,QuickLR::On);
  Event::Attach(EventZoomOut,QuickLR::Off);
}

if ($QuickLR::Key != $Zoom::Key)
  bind($QuickLR::Key,"QuickLR::On();","QuickLR::Off();",QuickLaserRifle);

function QuickLR::On()
{
  if (getItemCount($QuickLR::Weapon))
  {
    $QuickLR::ON = "TRUE";
    $QuickLR::PrevWep = getMountedItem(0);
    use($QuickLR::Weapon);
  }
}

function QuickLR::Off()
{
  if ($QuickLR::ON)
  {
    use($QuickLR::PrevWep);
    $QuickLR::ON = "FALSE";
    $QuickLR::PrevWep="";
  }
}