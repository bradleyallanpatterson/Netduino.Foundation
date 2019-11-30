using System;
using System.Threading;
using System.Threading.Tasks;
using Meadow;
using Meadow.Devices;


namespace PWM.Expander.PCA9685_Sample
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        PCA9685 pca9685;

        public MeadowApp()
        {
            Console.WriteLine("Initializing...");

            // configure our PCA9685 on the I2C Bus
            var i2c = Device.CreateI2cBus();
            pca9685 = new PCA9685(
                i2c,
                PCA9685.I2cAddress.Adddress0x77 //default
            );

            //// TODO: SPI version

            //// Example that uses an IObersvable subscription to only be notified
            //// when the temperature changes by at least a degree, and humidty by 5%.
            //// (blowing hot breath on the sensor should trigger)
            //pca9685.Subscribe(new FilterableObserver<AtmosphericConditionChangeResult, AtmosphericConditions>(
            //    h => {
            //        Console.WriteLine($"Temp and pressure changed by threshold; new temp: {h.New.Temperature}, old: {h.Old.Temperature}");
            //    },
            //    e => {
            //        return (
            //            (Math.Abs(e.Delta.Temperature) > 1)
            //            &&
            //            (Math.Abs(e.Delta.Pressure) > 5)
            //            );
            //    }
            //    ));

            //// classical .NET events can also be used:
            //pca9685.Updated += (object sender, AtmosphericConditionChangeResult e) => {
            //    Console.WriteLine($"  Temperature: {e.New.Temperature}ºC");
            //    Console.WriteLine($"  Pressure: {e.New.Pressure}hPa");
            //    Console.WriteLine($"  Relative Humidity: {e.New.Humidity}%");
            };


            // just for funsies.
            Console.WriteLine($"ChipID: {pca9685.GetChipID():X2}");
            //Thread.Sleep(1000);

            //// is this necessary? if so, it should probably be tucked into the driver
            //Console.WriteLine("Reset");
            //bme280.Reset();

            // get an initial reading
            //ReadConditions().Wait();

            // start updating continuously
            pca9685.StartUpdating();

        }

        protected async Task ReadConditions()
        {
            var conditions = await pca9685.Read();
            Console.WriteLine("Initial Readings:");
            Console.WriteLine($"  Temperature: {conditions.Temperature}ºC");
            Console.WriteLine($"  Pressure: {conditions.Pressure}hPa");
            Console.WriteLine($"  Relative Humidity: {conditions.Humidity}%");
        }

    }
}