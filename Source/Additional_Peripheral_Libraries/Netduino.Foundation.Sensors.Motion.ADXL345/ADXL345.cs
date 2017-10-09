﻿using System;
using Microsoft.SPOT;
using Netduino.Foundation.Core;

namespace Netduino.Foundation.Sensors.Motion
{
    /// <summary>
    /// Driver for the ADXL345 3-axis digital accelerometer capable of measuring
    //  up to +/-16g.
    /// </summary>
    public class ADXL345
    {
        #region Constants

        /// <summary>
        /// Enable Z-axis inactivity interrupt.
        /// </summary>
        public const byte ACTRL_Z_INACTIVITY_ENABLE = 0x01;

        /// <summary>
        /// Enable Y-axis inactivity interrupt.
        /// </summary>
        public const byte ACTRL_Y_INAVTIYITY_ENABLE = 0x02;

        /// <summary>
        /// Enable X-axis inactivity interrupt.
        /// </summary>
        public const byte ACTRL_X_INACTIVITY_ENABLE = 0x04;

        /// <summary>
        /// Make the inactivity interrupt AC-coupled.
        /// </summary>
        public const byte ACTRL_INACTIVITY_AC_COUPLED = 0x08;

        /// <summary>
        /// Enable Z-axis activity interrupt.
        /// </summary>
        public const byte ACTRL_Z_ACTIVITY_ENABLE = 0x10;

        /// <summary>
        /// Enable Y-axis activity interrupt.
        /// </summary>
        public const byte ACTRL_Y_ACTIVITY_ENABLE = 0x20;

        /// <summary>
        /// Enable X-axis activity interrupt.
        /// </summary>
        public const byte ACTRL_X_ACTIVITY_ENABLE = 0x40;

        /// <summary>
        /// Make the activity interrupt AC-coupled.
        /// </summary>
        public const byte ACTRL_ACTIVITY_AC_COUPLED = 0x80;

        #endregion Constants

        #region Enums

        /// <summary>
        /// Control registers for the ADXL345 chip.
        /// </summary>
        /// <remarks>
        /// Taken from table 19 on page 23 of the data sheet.
        /// </remarks>
        enum Registers : byte { DeviceID = 0x00, TAPThreshold = 0x1d, OffsetX = 0x1e, OffsetY = 0x1f, OffsetZ = 0x20,
                                TAPDuration = 0x21, TAPLatency = 0x22, TAPWindow = 0x23, ActivityThreshold = 0x24, 
                                InactivityThreshold = 0x25, InactivityTime = 0x26, ActivityInactivityControl = 0x27,
                                FreeFallThreshold = 0x28, FreeFallTime = 0x29, TAPAxes = 0x2a, TAPActivityStatus = 0x2a,
                                DataRate = 0x2c, PowerControl = 0x2d, InterruptEnable = 0x2e, InterruptMap = 0x2f,
                                InterruptSource = 0x30, DataFormat = 0x31, X0 = 0x32, X1 = 0x33, Y0 = 0x33, Y1 = 0x34,
                                Z0 = 0x36, Z1 = 0x37, FirstInForstOutControl = 0x38, FirstInFirstOut = 0x39};

        #endregion Enums

        #region Member variables / fields

        /// <summary>
        /// Communication bus used to communicate with the sensor.
        /// </summary>
        private ICommunicationBus _adxl345;

        #endregion Member variables / fileds

        #region Properties

        /// <summary>
        /// Get the device ID, this should return 0xe5.
        /// </summary>
        public byte DeviceID
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.DeviceID));
            }
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public int Z { get; private set; }

        /// <summary>
        /// Threshold for the tap interrupts (62.5 mg/LSB).  A value of 0 may lead to undesirable
        /// results and so will be rejected.
        /// </summary>
        public byte TapThreshold
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.TAPThreshold));
            }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Threshold should be between 1 and 255 inclusive.");
                }
                _adxl345.WriteRegister((byte) Registers.TAPThreshold, value);
            }
        }

        /// <summary>
        /// Values stored in this register are automatically added to the X reading.
        /// </summary>
        /// <remarks>
        /// Scale factor is 15.6 mg/LSB so 0x7f represents an offset of 2g.
        /// </remarks>
        public sbyte OffsetX
        {
            get
            {
                return ((sbyte) _adxl345.ReadRegister((byte) Registers.OffsetX));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.OffsetX, (byte) value);
            }
        }

        /// <summary>
        /// Values stored in this register are automatically added to the Y reading.
        /// </summary>
        /// <remarks>
        /// Scale factor is 15.6 mg/LSB so 0x7f represents an offset of 2g.
        /// </remarks>
        public sbyte OffsetY
        {
            get
            {
                return ((sbyte) _adxl345.ReadRegister((byte) Registers.OffsetY));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.OffsetY, (byte) value);
            }
        }

        /// <summary>
        /// Values stored in this register are automatically added to the Z reading.
        /// </summary>
        /// <remarks>
        /// Scale factor is 15.6 mg/LSB so 0x7f represents an offset of 2g.
        /// </remarks>
        public sbyte OffsetZ
        {
            get
            {
                return ((sbyte) _adxl345.ReadRegister((byte) Registers.OffsetZ));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.OffsetZ, (byte) value);
            }
        }

        /// <summary>
        /// The maximum time that an event must be above the threshold in order to qualify
        /// as a double tap event.  The scale factor is 625us per LSB.
        /// </summary>
        /// <remarks>
        /// A value of 0 disables the double tap function.
        /// </remarks>
        public byte Duration
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.TAPDuration));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.TAPDuration, value);
            }
        }

        /// <summary>
        /// Used in combination with the Window property to control the double tap detection.
        /// This value represents the period of time between the detection of the tap event
        /// until the start of the time window.
        ///
        /// The scale factor is 1.25ms per LSB.
        /// </summary>
        /// <remarks>
        /// A value of 0 disables the double tap function.
        /// </remarks>
        public byte DoubleTapLatency
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.TAPLatency));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.TAPLatency, value);
            }
        }

        /// <summary>
        /// Defines the period in which a second tap event can occur after the expiration
        /// of the latency period in order for the second table to be considered a
        /// double tap.  The time period is measured in 1.25ms per LSB.
        /// </summary>
        public byte DoubleTapWindow
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.TAPWindow));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.TAPWindow, value);
            }
        }

        /// <summary>
        /// Threshold for detecting activity as 62.5mg per LSB.
        /// </summary>
        /// <remarks>
        /// A value of zero is undesirable when interrupts are enabled.
        /// <remarks>
        public byte ActivityThreshold
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.ActivityThreshold));
            }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("ActivityThreshold should be between 1 and 255 inclusive.");
                }
                _adxl345.WriteRegister((byte) Registers.ActivityThreshold, value);
            }
        }

        /// <summary>
        /// Threshold valie for determining inactivity.
        /// </summary>
        /// <remarks>
        /// A value of 0 is not recommended when the inactivity interrupt is enabled.
        /// </remarks>
        public byte InactivityThreshold
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.InactivityThreshold));
            }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("InactivityThreshold should be between 1 and 255 inclusive.");
                }
                _adxl345.WriteRegister((byte) Registers.InactivityThreshold, value);
            }
        }

        /// <summary>
        /// The amount of time that the acceleration must be less than the threshold value
        /// in order for inactivity to be declared.
        /// </summary>
        /// <remarks>
        /// Scale factor is 1s per LSB.
        ///
        /// A value of 0 will allow an interrupt to be generated when the output data is
        /// less than the InactivityThreshold.
        /// <remarks>
        public byte InactivityTime
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.InactivityTime));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.InactivityTime, value);
            }
        }

        /// <summary>
        /// Set the Activity and Inactivity control
        /// </summary>
        /// <remarks>
        /// A setting of 0 selects dc-coupled operation, and a setting of 1 enables ac-coupled 
        /// operation. In dc-coupled operation, the current acceleration magnitude is compared 
        /// directly with THRESH_ACT and THRESH_INACT to determine whether activity or inactivity 
        /// is detected.
        /// 
        /// In ac-coupled operation for activity detection, the acceleration value at the start
        /// of activity detection is taken as a reference value. New samples of acceleration 
        /// are then compared to this reference value, and if the magnitude of the difference 
        /// exceeds the THRESH_ACT value, the device triggers an activity interrupt.
        /// 
        /// Similarly, in ac-coupled operation for inactivity detection, a reference value is 
        /// used for comparison and is updated whenever the device exceeds the inactivity
        /// threshold. After the reference value is selected, the device compares the magnitude 
        /// of the difference between the reference value and the current acceleration with 
        /// THRESH_INACT. If the difference is less than the value in THRESH_INACT for the time
        /// in TIME_INACT, the device is considered inactive and the inactivity interrupt is 
        /// triggered.
        /// 
        /// An activity interrupt is generated if any of the x, y, z axis have activity monitoring
        /// enabled and any of the axes register activity exceeding the threshold.
        /// 
        /// An inactivity interrupt is generated if any of the x, y or z axis have inactivity
        /// monitoring enabled and ALL of the enabled axes exceed the threshold.
        /// </remarks>
        public byte ActivityAndInactivityControl
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.ActivityInactivityControl));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.ActivityInactivityControl, value);
            }
        }

        /// <summary>
        /// Free-fall detection threshold value.
        ///
        /// Scale factor is 62.5mg per LSB.
        /// </summary>
        /// <remarks>
        /// A value fo 0 may result in undesirable behavior if free-fall interrupts
        /// are enabled.
        /// 
        /// Recommended value is between 300mg (0x05) and 600mg (0x09).
        /// <remarks>
        public byte FreeFallThreshold
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.FreeFallThreshold));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.FreeFallThreshold, value);
            }
        }

        /// <summary>
        /// The amount of time that all three axis must 
        /// </summary>
        /// <remarks>
        /// Scale factor is 5ms per LSB.
        ///
        /// A value of 0 may result in undesirable behavior if the free-fall
        /// interrupt is enabled.
        /// 
        /// Recommended value should be between 100ms (0x14) and 350ms (0x46);
        /// <remarks>
        public byte FreeFallTime
        {
            get
            {
                return (_adxl345.ReadRegister((byte) Registers.FreeFallTime));
            }
            set
            {
                _adxl345.WriteRegister((byte) Registers.FreeFallTime, value);
            }
        }

        /// <summary>
        /// Determine which interrupts are enabled / disabled.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public byte InterruptEnable { get; set; }

        /// <summary>
        ///
        /// </summary>
        public byte InterruptMap { get; set; }

        /// <summary>
        /// Indicate which interrupts have generated the interrupt.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        public byte InterruptSource { get; private set; }

        /// <summary>
        /// Determine the format of the data in the X0, X1, Y0, Y1, Z0 and Z1 registers.
        /// </summary>
        public byte DataFormat { get; set; }

        /// <summary>
        ///
        /// </summary>
        public byte FirstInFirstOutControl { get; set; }

        /// <summary>
        /// Register indicating if a First in First our event has occurred.
        /// </summary>
        public byte FirstInFirstOurStatus { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// </remarks>
        public byte BandwidthRate { get; set; }

        /// <summary>
        /// </summary>
        public byte PowerControl { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Make the default constructor private so that it cannot be used.
        /// </summary>
        private ADXL345()
        {
        }

        /// <summary>
        /// Create a new instance of the ADXL345 communicating over the I2C interface.
        /// </summary>
        /// <param name="address">Address of the I2C sensor</param>
        /// <param name="speed">Speed of the I2C bus in KHz</param>
        public ADXL345(byte address = 0x1d, ushort speed = 100)
        {
			if ((address != 0x1d) && (address != 0x53))
			{
                throw new ArgumentOutOfRangeException("address", "ADXL345 address can only be 0x1d or 0x53.");
			}
			if ((speed < 10) || (speed > 400))
			{
                throw new ArgumentOutOfRangeException("speed", "ADXL345 speed should be between 10 kHz and 400 kHz inclusive.");
			}
			I2CBus device = new I2CBus(address, speed);
			_adxl345 = (ICommunicationBus) device;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Read the six sensor bytes and set the values for the X, y and Z acceleration.
        /// </summary>
        /// <remarks>
        /// All six acceleration registers should be read at the same time to ensure consistency
        /// of the measurements.
        /// </remarks>
        public void Read()
        {

        }

        #endregion Methods

        #region Interrupt handlers

        #endregion Interrupt handlers
    }
}
