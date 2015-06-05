// ============================================================================
// Basic Stack Functionality
// ============================================================================

function Stack::Push( %label, %item )
{
  $Stack::[ %label, count ]++;
  $Stack::[ %label, $Stack::[ %label, count ] ] = %item;
}

function Stack::Pop( %label )
{
  if ( $Stack::[ %label, count ] )
  {
    %item = $Stack::[ %label, $Stack::[ %label, count ] ];
    $Stack::[ %label, $Stack::[ %label, count ] ] = "";
    $Stack::[ %label, count ]--;
    return %item;
  }
  return "FALSE";
}

function Stack::Count( %label )
{
  return $Stack::[ %label, count ];
}