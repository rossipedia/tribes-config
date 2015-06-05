Event::Attach(EventConnected,INV::Connected);

function INV::Add(%name,%ammo,%pickupname)
{
  for (%i=1;%i<=$INV::ListCount;%i++)
  {
    if ($INV::NameList[%i] == %name)
      return;
  }
  $INV::ListCount = %i;
  $INV::NameList[%i] = %name;
  $INV::AmmoList[%i] = %ammo;
  $INV::PUNameList[%i] = %pickupname;
}

function INV::_Add(%name,%ammo,%pickupname)
{
  if ((%num = getItemType(%name))!=-1)
  {
    $INV::Name[%num] = %name;
    $INV::Num[%name] = %num;
    if (%ammo!="")
    {
      %anum = getItemType(%ammo);
      if (%anum==-1) { echo("Invalid Item: "@%ammo); return; }
      INV::_Add(%ammo);
      $INV::Ammo[%name] = %anum;
      $INV::Ammo[%num] = %anum;
    }
    if (%pickupname!="")
      $INV::Num[%pickupname] = %num;
  }
  else
    echo("Item: "@%name@" does not exist on this server.");
}

function INV::Connected()
{
  for(%i=1;%i<$INV::ListCount;%i++)
  {
    //echo($INV::NameList[%i]);
    INV::_Add(
      $INV::NameList[%i],
      $INV::AmmoList[%i],
      $INV::PUNameList[%i]
    );
  }
}






INV::Add("Light Armor");
INV::Add("Medium Armor");
INV::Add("Heavy Armor");

INV::Add("Blaster");
INV::Add("Plasma Gun","Plasma Bolt");
INV::Add("Chaingun","Bullet");
INV::Add("Disc Launcher","Disc");
INV::Add("Grenade Launcher","Grenade Ammo");
INV::Add("Laser Rifle");
INV::Add("ELF Gun");
INV::Add("Mortar","Mortar Ammo");

INV::Add("Energy Pack","","EnergyPack");
INV::Add("Repair Pack","","RepairPack");
INV::Add("Shield Pack","","ShieldPack");
INV::Add("Sensor Jammer Pack","","SensorJammerPack");
INV::Add("Ammo Pack","","Ammopack");

INV::Add("Inventory Station","","DeployableInvPack");
INV::Add("Ammo Station","","DeployableAmmoPack");
INV::Add("Motion Sensor","","MotionSensorPack");
INV::Add("Pulse Sensor","","PulseSensorPack");
INV::Add("Camera","","CameraPack");
INV::Add("Sensor Jammer","","DeployableSensorJammerPack");
INV::Add("Turret","","TurretPack");

INV::Add("Beacon");
INV::Add("Mine");
INV::Add("Grenade");
INV::Add("Repair Kit");
INV::Add("Targeting Laser");

INV::Add("Scout");
INV::Add("LPC");
INV::Add("HPC");



INV::Add("Light Armor");
INV::Add("Medium Armor");
INV::Add("Heavy Armor");

INV::Add("Blaster");
INV::Add("Plasma Gun","Plasma Bolt");
INV::Add("Chaingun","Bullet");
INV::Add("Disc Launcher","Disc");
INV::Add("Grenade Launcher","Grenade Ammo");
INV::Add("Laser Rifle");
INV::Add("ELF Gun");
INV::Add("Mortar","Mortar Ammo");

INV::Add("Energy Pack","","EnergyPack");
INV::Add("Repair Pack","","RepairPack");
INV::Add("Shield Pack","","ShieldPack");
INV::Add("Sensor Jammer Pack","","SensorJammerPack");
INV::Add("Ammo Pack","","Ammopack");

INV::Add("Inventory Station","","DeployableInvPack");
INV::Add("Ammo Station","","DeployableAmmoPack");
INV::Add("Motion Sensor","","MotionSensorPack");
INV::Add("Pulse Sensor","","PulseSensorPack");
INV::Add("Camera","","CameraPack");
INV::Add("Sensor Jammer","","DeployableSensorJammerPack");
INV::Add("Turret","","TurretPack");

INV::Add("Beacon");
INV::Add("Mine");
INV::Add("Grenade");
INV::Add("Repair Kit");
INV::Add("Targeting Laser");

INV::Add("Scout");
INV::Add("LPC");
INV::Add("HPC");


INV::Add("Firestorm Bomber");
INV::Add("Stealth LPC");
INV::Add("MAG Gun");
INV::Add("Pyro-Torch",  "Pyro Charge");
INV::Add("IX-2000 Sniper Rifle",  "Rifle Ammo");
INV::Add("Shotgun",  "Shotgun Shells");
INV::Add("Phalanxx Cannon",  "Phalanxx Ammo");
INV::Add("Rocket Launcher",  "RocketAmmo");
INV::Add("EMP Grenade Launcher",  "EMPGrenadeAmmo");
INV::Add("Heavy Thruster"); 
INV::Add("Medium Thruster");
INV::Add("Small Force Field");
INV::Add("Sentry");
INV::Add("Avenger");
INV::Add("Flak Cannon");
INV::Add("Cloaking Device");
INV::Add("Vengeance Missile Pack");
INV::Add("Rocket Booster"); 
INV::Add("Heat Sink");


