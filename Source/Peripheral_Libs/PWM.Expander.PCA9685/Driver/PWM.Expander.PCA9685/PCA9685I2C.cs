﻿using System;
using Meadow.Hardware;

namespace Meadow.Foundation.PWM.Expander
{
    internal class PCA9685I2C : PCA9685Comms
    {
        private II2cBus _i2c;
        private byte _address;

        internal PCA9685I2C(II2cBus i2c, byte busAddress)
        {
            if ((busAddress != 0x76) && (busAddress != 0x77))
            {
                throw new ArgumentOutOfRangeException(nameof(busAddress), "Address should be 0x76 or 0x77");
            }

            _i2c = i2c;
            _address = busAddress;
        }

        public override byte[] ReadRegisters(byte startRegister, int readCount)
        {
            return _i2c.WriteReadData(_address, readCount, (byte)startRegister);
        }

        public override void WriteRegister(Register register, byte value)
        {
            _i2c.WriteData(_address, (byte)register, value);
        }
    }
}