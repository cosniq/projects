################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../src/device/BDK.c \
../src/device/BDK_Task.c \
../src/device/EventCallback.c \
../src/device/HAL.c \
../src/device/HAL_I2C.c \
../src/device/HAL_UART.c \
../src/device/HAL_clock.c \
../src/device/HAL_error.c \
../src/device/I2C_RSLxx.c \
../src/device/SEGGER_RTT.c \
../src/device/SEGGER_RTT_printf.c \
../src/device/SoftwareTimer.c \
../src/device/USART_RSLxx.c \
../src/device/stimer.c \
../src/device/syscalls_hal_uart.c 

OBJS += \
./src/device/BDK.o \
./src/device/BDK_Task.o \
./src/device/EventCallback.o \
./src/device/HAL.o \
./src/device/HAL_I2C.o \
./src/device/HAL_UART.o \
./src/device/HAL_clock.o \
./src/device/HAL_error.o \
./src/device/I2C_RSLxx.o \
./src/device/SEGGER_RTT.o \
./src/device/SEGGER_RTT_printf.o \
./src/device/SoftwareTimer.o \
./src/device/USART_RSLxx.o \
./src/device/stimer.o \
./src/device/syscalls_hal_uart.o 

C_DEPS += \
./src/device/BDK.d \
./src/device/BDK_Task.d \
./src/device/EventCallback.d \
./src/device/HAL.d \
./src/device/HAL_I2C.d \
./src/device/HAL_UART.d \
./src/device/HAL_clock.d \
./src/device/HAL_error.d \
./src/device/I2C_RSLxx.d \
./src/device/SEGGER_RTT.d \
./src/device/SEGGER_RTT_printf.d \
./src/device/SoftwareTimer.d \
./src/device/USART_RSLxx.d \
./src/device/stimer.d \
./src/device/syscalls_hal_uart.d 


# Each subdirectory must supply rules for building sources it contributes
src/device/%.o: ../src/device/%.c src/device/subdir.mk
	@echo 'Building file: $<'
	@echo 'Invoking: GNU ARM Cross C Compiler'
	arm-none-eabi-gcc -mcpu=cortex-m3 -mthumb -mlittle-endian -Os -fmessage-length=0 -fsigned-char -ffunction-sections -fdata-sections -Wunused -Wuninitialized -Wall -Wshadow  -g -DCFG_PRF_BASS -DRSL10_CID=101 -DCFG_ALLPRF -DCFG_ALLROLES -DCFG_APP -DCFG_ATTS -DCFG_BLE=1 -DCFG_SLEEP -DCFG_HW_AUDIO -DCFG_CHNL_ASSESS -DCFG_CON=1 -DCFG_EMB -DCFG_EXT_DB -DCFG_HOST -DCFG_NB_PRF=2 -DCFG_PRF -DCFG_RF_ATLAS -DCFG_SEC_CON -DAPP_TRACE_DISABLED=1 -D_RTE_ -I"D:\OnSemiV2\sense_ics_firmware_sleep\include" -I"D:\OnSemiV2\sense_ics_firmware_sleep\include\bdk" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components/bhy1_firmware" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/bb" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble/profiles" -I"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/kernel" -I"D:\OnSemiV2\sense_ics_firmware_sleep/RTE" -I"D:\OnSemiV2\sense_ics_firmware_sleep/RTE/Device/RSL10" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/BDK/1.19.0/include/components/bhy1_firmware" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/bb" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/ble/profiles" -isystem"C:/Users/Nicu/AppData/Local/Arm/Packs/ONSemiconductor/RSL10/3.6.465/include/kernel" -isystem"D:\OnSemiV2\sense_ics_firmware_sleep/RTE" -isystem"D:\OnSemiV2\sense_ics_firmware_sleep/RTE/Device/RSL10" -std=gnu11 -MMD -MP -MF"$(@:%.o=%.d)" -MT"$@" -c -o "$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '


