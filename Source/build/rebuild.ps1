#
#   Clean the full solution and rebuild all of the Release version of the projects.
#
msbuild Netduino.Foundation.Peripherals.sln /property:Configuration=Release /t:Clean
msbuild Netduino.Foundation.Peripherals.sln /property:Configuration=Release
#
#   Regenerate all of the packages
#
nuget pack Netduino.Foundation/Netduino.Foundation.nuspec -NoDefaultExcludes
nuget pack Peripheral_Libs/Displays.GraphicsLibrary/Driver/Displays.GraphicsLibrary.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Displays.MicroLiquidCrystal/Driver/Displays.MicroLiquidCrystal.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Displays.SerialLCD/Driver/Displays.SerialLCD.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Displays.SSD1306/Driver/Displays.SSD1306.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/ICs.74595/Driver/ICs.74595.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/ICs.EEPROM.AT24Cxx/Driver/ICs.EEPROM.AT24Cxx.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/RTCs.DS323x/Driver/RTCs.DS323x.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Shields.AdafruitMotorShield/Driver/Shields.AdafruitMotorShield.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Atmospheric.BME280/Driver/Sensors.Atmospheric.BME280.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Atmospheric.BMP085/Driver/Sensors.Atmospheric.BMP085.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Atmospheric.GroveTH02/Driver/Sensors.Atmospheric.GroveTH02.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Atmospheric.HIH6130/Driver/Sensors.Atmospheric.HIH6130.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Atmospheric.HTU21DF/Driver/Sensors.Atmospheric.HTU21DF.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Barometric.MPL115A2/Driver/Sensors.Barometric.MPL115A2.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Barometric.MPL3115A2/Driver/Sensors.Barometric.MPL3115A2.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Atmospheric.SHT31D/Driver/Sensors.Atmospheric.SHT31D.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Atmospheric.SI7021/Driver/Sensors.Atmospheric.SI7021.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Compass.Grove3AxisDigitalCompass/Driver/Sensors.Compass.Grove3AxisDigitalCompass.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Distance.SharpGP2D12/Driver/Sensors.Distance.SharpGP2D12.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.GPS.NMEA/Driver/Sensors.GPS.NMEA.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Light.ALSPT19315C/Driver/Sensors.Light.ALSPT19315C.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Light.SI1145/Driver/Sensors.Light.SI1145.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Light.TSL2561/Driver/Sensors.Light.TSL2561.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Motion.ADXL335/Driver/Sensors.Motion.ADXL335.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Motion.ADXL345/Driver/Sensors.Motion.ADXL345.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Motion.ADXL362/Driver/Sensors.Motion.ADXL362.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Motion.BNO055/Driver/Sensors.Motion.BNO055.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Motion.FXAS21002/Driver/Sensors.Motion.FXAS21002.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Motion.FXOS8700CQ/Driver/Sensors.Motion.FXOS8700CQ.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Motion.MAG3110/Driver/Sensors.Motion.MAG3110.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Motion.Memsic2125/Driver/Sensors.Motion.Memsic2125.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Motion.MPU6050/Driver/Sensors.Motion.MPU6050.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Temperature.Analog/Driver/Sensors.Temperature.Analog.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Temperature.DS18B20/Driver/Sensors.Temperature.DS18B20.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Sensors.Temperature.TMP102/Driver/Sensors.Temperature.TMP102.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Servos.Servo/Driver/Servos.Servo.Core.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Shields.AdafruitMotorShield/Driver/Shields.AdafruitMotorShield.csproj -NoDefaultExcludes -Prop Configuration=Release
nuget pack Peripheral_Libs/Shields.SparkfunWeatherShield/Driver/Shields.SparkfunWeatherShield.csproj -NoDefaultExcludes -Prop Configuration=Release
#
#   Command below moves the packages.
#
#nuget init c:\NuGetPackages