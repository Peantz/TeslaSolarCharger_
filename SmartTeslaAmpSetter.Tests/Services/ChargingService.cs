﻿using System;
using Autofac;
using SmartTeslaAmpSetter.Shared.Dtos.Settings;
using SmartTeslaAmpSetter.Shared.Enums;
using SmartTeslaAmpSetter.Shared.TimeProviding;
using Xunit;
using Xunit.Abstractions;

namespace SmartTeslaAmpSetter.Tests.Services;

public class ChargingService : TestBase
{
    public ChargingService(ITestOutputHelper outputHelper)
        : base(outputHelper)
    {
    }

    [Theory]
    [InlineData(ChargeMode.PvAndMinSoc, -32, 10, false)]
    [InlineData(ChargeMode.PvAndMinSoc, 2, -10, false)]
    [InlineData(ChargeMode.PvOnly, -32, 10, false)]
    [InlineData(ChargeMode.PvOnly, 2, -10, false)]
    [InlineData(ChargeMode.PvAndMinSoc, -32, 10, true)]
    [InlineData(ChargeMode.PvAndMinSoc, 2, -10, true)]
    [InlineData(ChargeMode.PvOnly, -32, 10, true)]
    [InlineData(ChargeMode.PvOnly, 2, -10, true)]
    public void Does_autoenable_fullspeed_charge_if_needed(ChargeMode chargeMode, int fullSpeedChargeMinutesAfterLatestTime, int moreSocThanMinSoc, bool autofullSpeedCharge)
    {
        var chargingService = Mock.Create<Server.Services.ChargingService>();
        var currentTimeProvider = Mock.Create<FakeDateTimeProvider>(
            new NamedParameter("dateTime", new DateTime(2022, 4, 1, 14, 0, 0)));
        var currentTime = currentTimeProvider.Now();

        var timeSpanToLatestTimeToReachMinSoc = TimeSpan.FromMinutes(60);
        var timeSpanToReachMinSoCAtFullSpeedCharge = timeSpanToLatestTimeToReachMinSoc.Add(TimeSpan.FromMinutes(fullSpeedChargeMinutesAfterLatestTime));

        var minSoc = 50;

        var car = CreateDemoCar(chargeMode, currentTime + timeSpanToLatestTimeToReachMinSoc, minSoc + moreSocThanMinSoc, minSoc, autofullSpeedCharge);


        var reachedMinimumSocAtFullSpeedChargeDateTime = currentTime + timeSpanToReachMinSoCAtFullSpeedCharge;

        chargingService.EnableFullSpeedChargeIfMinimumSocNotReachable(car, reachedMinimumSocAtFullSpeedChargeDateTime);
        chargingService.DisableFullSpeedChargeIfMinimumSocReachedOrMinimumSocReachable(car, reachedMinimumSocAtFullSpeedChargeDateTime);


        if (fullSpeedChargeMinutesAfterLatestTime > 0)
        {
            Assert.True(car.CarState.AutoFullSpeedCharge);
            return;
        }

        if (moreSocThanMinSoc >= 0)
        {
            Assert.False(car.CarState.AutoFullSpeedCharge);
            return;
        }

        switch (chargeMode)
        {
            case ChargeMode.PvAndMinSoc:
                Assert.True(car.CarState.AutoFullSpeedCharge);
                break;

            case ChargeMode.PvOnly:
                Assert.False(car.CarState.AutoFullSpeedCharge);
                break;

            default:
                throw new NotImplementedException("This test does not handle this charge mode");
        }

    }

    private Car CreateDemoCar(ChargeMode chargeMode, DateTime latestTimeToReachSoC, int soC, int minimumSoC, bool autoFullSpeedCharge)
    {
        var car = new Car()
        {
            CarState = new CarState()
            {
                AutoFullSpeedCharge = autoFullSpeedCharge,
                SoC = soC,
            },
            CarConfiguration = new CarConfiguration()
            {
                LatestTimeToReachSoC = latestTimeToReachSoC,
                MinimumSoC = minimumSoC,
                ChargeMode = chargeMode,
            },
        };
        return car;
    }
}