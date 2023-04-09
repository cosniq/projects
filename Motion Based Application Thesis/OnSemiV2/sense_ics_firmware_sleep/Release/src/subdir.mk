################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../src/CSN_LP_ALS.c \
../src/CSN_LP_AO.c \
../src/CSN_LP_ENV.c \
../src/HAL_RTC.c \
../src/aes.c \
../src/app.c \
../src/app_ble_hooks.c \
../src/app_init.c \
../src/app_sleep.c \
../src/app_timer.c \
../src/app_trace.c \
../src/calibration.c 

S_UPPER_SRCS += \
../src/wakeup_asm.S 

OBJS += \
./src/CSN_LP_ALS.o \
./src/CSN_LP_AO.o \
./src/CSN_LP_ENV.o \
./src/HAL_RTC.o \
./src/aes.o \
./src/app.o \
./src/app_ble_hooks.o \
./src/app_init.o \
./src/app_sleep.o \
./src/app_timer.o \
./src/app_trace.o \
./src/calibration.o \
./src/wakeup_asm.o 

S_UPPER_DEPS += \
./src/wakeup_asm.d 

C_DEPS += \
./src/CSN_LP_ALS.d \
./src/CSN_LP_AO.d \
./src/CSN_LP_ENV.d \
./src/HAL_RTC.d \
./src/aes.d \
./src/app.d \
./src/app_ble_hooks.d \
./src/app_init.d \
./src/app_sleep.d \
./src/app_timer.d \
./src/app_trace.d \
./src/calibration.d 


# Each subdirectory must supply rules for building sources it contributes
src/%.o: ../src/%.c src/subdir.mk
	@echo 'Building file: $<'
	@echo 'Invoking: GNU ARM Cross C Compiler'
	arm-none-eabi-gcc -mcpu=cortex-m3 -mthumb -mlittle-endian -Os -fmessage-length=0 -fsigned-char -ffunction-sections -fdata-sections -Wunused -Wuninitialized -Wall -Wshadow  -g -DCFG_PRF_BASS -DRSL10_CID=101 -DCFG_ALLPRF -DCFG_ALLROLES -DCFG_APP -DCFG_ATTS -DCFG_BLE=1 -DCFG_SLEEP -DCFG_HW_AUDIO -DCFG_CHNL_ASSESS -DCFG_CON=1 -DCFG_EMB -DCFG_EXT_DB -DCFG_HOST -DCFG_NB_PRF=2 -DCFG_PRF -DCFG_RF_ATLAS -DCFG_SEC_CON -DAPP_TRACE_DISABLED=1 -D_RTE_ -I"D:\OnSemiV2\sense_ics_firmware_sleep\include" -I"D:\OnSemiV2\sense_ics_firmware_sleep\include\bdk" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components/bhy1_firmware" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/bb" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble/profiles" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/kernel" -I"D:\OnSemiV2\sense_ics_firmware_sleep/RTE" -I"D:\OnSemiV2\sense_ics_firmware_sleep/RTE/Device/RSL10" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components/bhy1_firmware" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/bb" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble/profiles" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/kernel" -isystem"D:\OnSemiV2\sense_ics_firmware_sleep/RTE" -isystem"D:\OnSemiV2\sense_ics_firmware_sleep/RTE/Device/RSL10" -std=gnu11 -MMD -MP -MF"$(@:%.o=%.d)" -MT"$@" -c -o "$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '

src/%.o: ../src/%.S src/subdir.mk
	@echo 'Building file: $<'
	@echo 'Invoking: GNU ARM Cross Assembler'
	arm-none-eabi-gcc -mcpu=cortex-m3 -mthumb -mlittle-endian -Os -fmessage-length=0 -fsigned-char -ffunction-sections -fdata-sections -Wunused -Wuninitialized -Wall -Wshadow  -g -x assembler-with-cpp -D_RTE_ -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components/bhy1_firmware" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/bb" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble/profiles" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/kernel" -I"D:\OnSemiV2\sense_ics_firmware_sleep/RTE" -I"D:\OnSemiV2\sense_ics_firmware_sleep/RTE/Device/RSL10" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components/bhy1_firmware" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/bb" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble/profiles" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/kernel" -isystem"D:\OnSemiV2\sense_ics_firmware_sleep/RTE" -isystem"D:\OnSemiV2\sense_ics_firmware_sleep/RTE/Device/RSL10" -MMD -MP -MF"$(@:%.o=%.d)" -MT"$@" -c -o "$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '


