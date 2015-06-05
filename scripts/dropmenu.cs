// ============================================================================

// Drop Menu Key

$DM::Key = "alt d";
// ============================================================================


bind( $DM::Key, "DM::Show();", "", SHOW_DROP_MENU );


function DM::Show()
{
  %keys = "0123456789abcdefghijklmnopqrstuvwxyz,.;'/";
  %keynum = 0;
  %menu = "Drop Menu";
  MS::NewMenu( %menu );
  for( %i = 1; %i < $INV::ListCount; %i++ )
  {
    %name = $INV::NameList[ %i ];
    if ( getItemType( %name ) != -1 && getItemCount( %name ) )
    {
      // You have the item
      %key = String::GetSubStr( %keys, %keynum, 1 );
      %keynum++;
      MS::AddChoice( %menu, %key, $INV::NameList[ %i ], "drop( \"" @ $INV::NameList[ %i ] @ "\" );" );
      if ( $INV::AmmoList[ %i ] != "" && getItemCount( $INV::AmmoList[ %i ] ) )
      {
        %key = String::GetSubStr( %keys, %keynum, 1 );
        %keynum++;
        MS::AddChoice( %menu, %key, $INV::AmmoList[ %i ], "drop( \"" @ $INV::AmmoList[ %i ] @ "\" );" );
      }
    }
  }
  MS::Display( %menu );
}