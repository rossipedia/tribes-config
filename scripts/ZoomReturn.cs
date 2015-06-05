// ==========================================================================
// Zoom Return
// Returns to a predefined zoom level after zooming out or changing zoom.
$ZRet::ChangeDelay = "5";
$ZRet::OutDelay = "0";
// ==========================================================================

Event::Attach(EventZoomChange,ZRet::OnZoomChange);
Event::Attach(EventZoomOut,ZRet::OnZoomOut);

function ZRet::OnZoomOut(%level)
{
  Schedule::Add("Zoom::Set($Zoom::Default);",$ZRet::OutDelay);
}

function ZRet::OnZoomChange(%to,%from)
{
  Schedule::Add("ZRet::CheckZoomed();",$ZRet::ChangeDelay);    
}

function ZRet::CheckZoomed()
{
  if (!$Zoomed)
    Zoom::Set($Zoom::Default);
}