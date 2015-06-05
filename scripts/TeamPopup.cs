// ============================================================================
// File:				TeamPopup.cs
// Author:			Bryan "daerid" Ross
// Date:				3/7/2001
// Version:			1.0
// Description:	Displays a popup when you press a button that lists all your
//              current team members. Disappears when you release the button.
// ============================================================================

// ============================================================================
// USER CONFIGURATION SECTION
// ============================================================================

// Location:
// T = Top of screen
// C = Center of screen
// B = Bottom of screen
$TPU::Location = "B";


// The key you want to use.
bind("control q","TPU::Show();","TPU::Hide();");

// ============================================================================
// SCRIPTING SECTION
// ============================================================================

function TPU::Show()
{
  for ( %team = -1; $Team::Name[%team] != ""; %team++ )
  {
    %title = "<F0>Team <F1>" @ $Team::Name[%team] @ "<F0> ( <F2>" @ ($Team::Count[%team]+0) @ "<F0> )";
    if ( %team == Client::GetTeam( GetManagerID() ) )
      %title = %title @ "   <F2>-= FRIENDLY =-<F0>";
    %title = %title @ "\n";
    for ( %idx = 1; $Team::Client[%team,%idx] != ""; %idx++ )
    {
      %name = Client::GetName($Team::Client[%team,%idx]);
      // Fix invalid characters
      %name = String::Replace(%name,"<","<<");
      %name = String::Replace(%name,"\\","\\\\");
      %text[%team] = %text[%team] @ "\t<F1>" @ %idx @ ".\t<F2>" @ %name @ "\n";
    }
    %text = %text @ %title @ %text[%team] @ "\n";
  }
  eval( "remote" @ $TPU::Location @ "P(2048, %text);");
}

function TPU::Hide()
{
  remoteCP(2048, "");
}