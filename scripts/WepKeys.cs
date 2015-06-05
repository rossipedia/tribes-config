function WK::Set(%key,%w1,%w2,%mod)
{
  $WK::Wep[%key,1@%mod] = %w1;
  $WK::Wep[%key,2@%mod] = %w2;
  bind(%key, "WK::Use("@%key@");");
}

function WK::Use(%key)
{
  if (
    getItemCount($WK::Wep[%key,1@$ServerFavoritesKey]) && 
    getMountedItem(0) != getItemType($WK::Wep[%key,1@$ServerFavoritesKey])
    )
  {
    use($WK::Wep[%key,1@$ServerFavoritesKey]);
  }
  else if (
    getItemCount($WK::Wep[%key,2@$ServerFavoritesKey]) && 
    getMountedItem(0) != getItemType($WK::Wep[%key,2@$ServerFavoritesKey])
  )
  {
    use($WK::Wep[%key,2@$ServerFavoritesKey]);
  }
}

// ==========================================================================
// Set 'em

// Base
WK::Set("4","Disc Launcher","Plasma Gun");
WK::Set("5","Grenade Launcher","Mortar");
WK::Set("3","Chaingun","Laser Rifle");
WK::Set("2","ELF Gun","Targeting Laser");
WK::Set("1","Blaster","");


// =================================================================
// Use previous weapon on itemreceived
Event::Attach(EventItemReceived, WepCheck::Received);
function WepCheck::Received(%item,%amt)
{
  %cur = getMountedItem(0);
  if ( ( $INV::Num[%item] != %cur ) &&  ( %cur != -1 ) )
  {
    use(%cur);
    Schedule::Add("use(\"" @ %cur @ "\");",0.1,WEP_CHECK);
  }
}
// =================================================================