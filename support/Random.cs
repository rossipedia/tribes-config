if (IsFile("Config\\RandSeed.cs"))
  exec("RandSeed.cs");

if ($Rand::Seed=="")
 $Rand::Seed = getRandom();

for (%i=1;%i<=floor($Rand::Seed);%i++)
  %tmp = getRandom();

function Random::Exit()
{
  $Rand::Seed = getSimTime()*($Rand::Seed%200);
  export("Rand::Seed","config\\RandSeed.cs");
}
Event::Attach(EventExit,Random::Exit);


function Random::FromArr(%arrname,%min,%max)
{
  if (%max == "")
  {
    %max = %min;
    %min = 0;
  }
  %i = floor(getRandom() * (%max - %min)) + %min;
  eval("%val = $" @ %arrname @ "[%i];");
  return %val;
}