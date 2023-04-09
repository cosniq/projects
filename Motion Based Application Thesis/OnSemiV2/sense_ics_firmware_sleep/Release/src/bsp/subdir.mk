################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../src/bsp/BHI160_NDOF.c \
../src/bsp/BME680_ENV.c \
../src/bsp/BSEC_ENV.c \
../src/bsp/I2CEeprom.c \
../src/bsp/NOA1305_ALS.c \
../src/bsp/bhy_support.c \
../src/bsp/button_api.c \
../src/bsp/led_api.c 

OBJS += \
./src/bsp/BHI160_NDOF.o \
./src/bsp/BME680_ENV.o \
./src/bsp/BSEC_ENV.o \
./src/bsp/I2CEeprom.o \
./src/bsp/NOA1305_ALS.o \
./src/bsp/bhy_support.o \
./src/bsp/button_api.o \
./src/bsp/led_api.o 

C_DEPS += \
./src/bsp/BHI160_NDOF.d \
./src/bsp/BME680_ENV.d \
./src/bsp/BSEC_ENV.d \
./src/bsp/I2CEeprom.d \
./src/bsp/NOA1305_ALS.d \
./src/bsp/bhy_support.d \
./src/bsp/button_api.d \
./src/bsp/led_api.d 


# Each subdirectory must supply rules for building sources it contributes
src/bsp/%.o: ../src/bsp/%.c src/bsp/subdir.mk
	@echo 'Building file: $<'
	@echo 'Invoking: GNU ARM Cross C Compiler'
	arm-none-eabi-gcc -mcpu=cortex-m3 -mthumb -mlittle-endian -Os -fmessage-length=0 -fsigned-char -ffunction-sections -fdata-sections -Wunused -Wuninitialized -Wall -Wshadow  -g -DCFG_PRF_BASS -DRSL10_CID=101 -DCFG_ALLPRF -DCFG_ALLROLES -DCFG_APP -DCFG_ATTS -DCFG_BLE=1 -DCFG_SLEEP -DCFG_HW_AUDIO -DCFG_CHNL_ASSESS -DCFG_CON=1 -DCFG_EMB -DCFG_EXT_DB -DCFG_HOST -DCFG_NB_PRF=2 -DCFG_PRF -DCFG_RF_ATLAS -DCFG_SEC_CON -DAPP_TRACE_DISABLED=1 -D_RTE_ -I"D:\OnSemiV2\sense_ics_firmware_sleep\include" -I"D:\OnSemiV2\sense_ics_firmware_sleep\include\bdk" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components/bhy1_firmware" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/bb" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble/profiles" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/kernel" -I"D:\OnSemiV2\sense_ics_firmware_sleep/RTE" -I"D:\OnSemiV2\sense_ics_firmware_sleep/RTE/Device/RSL10" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components/bhy1_firmware" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/bb" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble/profiles" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/kernel" -isystem"D:\OnSemiV2\sense_ics_firmware_sleep/RTE" -isystem"D:\OnSemiV2\sense_ics_firmware_sleep/RTE/Device/RSL10" -std=gnu11 -MMD -MP -MF"$(@:%.o=%.d)" -MT"$@" -c -o "$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '


