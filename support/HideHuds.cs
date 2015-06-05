function hideHuds()
{
  $showHudCount = 0;
  %playGuiID = nameToId(playGui);
  %objs = Group::objectCount(%playGuiID);
	echo( "hiding hud: " @ %objs );
  for(%i = 0; %i < %objs; %i++) 
  {
    %objid = Group::getObject(%playGuiID, %i);
    if( Control::getVisible(Object::getName(%objid)) )
    {
			echo( "hiding hud: " @ Object::getName(%objid) );
      Control::setVisible(Object::getName(%objid), false);
      $showHudCount++;
      $hudsToShow[ $showHudCount ] = %objid;
    }
  }
}

function showHuds()
{
  for( %i = 1; %i <= $showHudCount; %i++ )
  {
    %obj = $hudsToShow[ %i ];
		echo( "Showing HUD: " @ Object::getName( %obj ) );
    Control::setVisible( Object::getName(%obj), true );
  }
}