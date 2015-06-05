// ==========================================================================
// ZoomTapping
// Enables tapping of the zoom key to stay zoomed in (press again to zoom out)

// The tapping threshold
$ZT::TapTime = "0.025";
// 0 = disabled
// 1 = tap once
// 2 = double tap
$ZT::TapType = 1;
// ==========================================================================

Event::Attach(EventZoomPressed,ZT::OnPressed);
Event::Attach(EventZoomReleased,ZT::OnReleased);

function ZT::OnPressed()
{
  if ($Zoomed)
  {
    Zoom::Out();
    $ZT::NoRelease = "";
    return "FALSE";
  }
  if ($ZT::TapType==1)
  {
    Tap::Start(ZOOMTAP,$ZT::TapTime);
  }
  else if ($ZT::TapType==2)
  {
    DoubleTap::Start(ZOOMDBLTAP,$ZT::TapTime);
    if (DoubleTapped(ZOOMDBLTAP))
      $ZT::NoRelease = "TRUE";
    
  }
}

function ZT::OnReleased()
{
  if ($ZT::TapType==1)
  {
    if (tapped(ZOOMTAP))
      return "FALSE";
  }
  else if ($ZT::TapType==2)
  {
    if ($ZT::NoRelease)
      return "FALSE";
  }
}