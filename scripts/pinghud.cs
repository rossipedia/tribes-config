function PingHud::GetPing( %ping )
{
  echo( "Ping Is: " @ %ping );
}
Event::Attach( EventPing, PingHud::GetPing );