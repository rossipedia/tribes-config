function HKits::StationOn(%remote)
{
  if (%remote) { return; }
  for (%i=0;%i<=6;%i++)
  {
    remoteEval(2048, useItem, getItemType("Repair Kit"));
		remoteEval(2048, buyItem,  getItemType("Repair Kit"));
  }
}
Event::Attach(EventAtStation,HKits::StationOn);