################################################################################
# Automatically-generated file. Do not edit!
################################################################################

-include ../makefile.init

RM := rm -rf

# All of the sources participating in the build are defined here
-include sources.mk
-include src/ics/subdir.mk
-include src/device/subdir.mk
-include src/bsp/subdir.mk
-include src/ble/subdir.mk
-include src/subdir.mk
-include RTE/Device/RSL10/subdir.mk
-include RTE/Components/subdir.mk
-include subdir.mk
-include objects.mk

ifneq ($(MAKECMDGOALS),clean)
ifneq ($(strip $(ASM_DEPS)),)
-include $(ASM_DEPS)
endif
ifneq ($(strip $(S_UPPER_DEPS)),)
-include $(S_UPPER_DEPS)
endif
ifneq ($(strip $(C_DEPS)),)
-include $(C_DEPS)
endif
endif

-include ../makefile.defs

OPTIONAL_TOOL_DEPS := \
$(wildcard ../makefile.defs) \
$(wildcard ../makefile.init) \
$(wildcard ../makefile.targets) \


BUILD_ARTIFACT_NAME := sense_ics_firmware_sleep
BUILD_ARTIFACT_EXTENSION := elf
BUILD_ARTIFACT_PREFIX :=
BUILD_ARTIFACT := $(BUILD_ARTIFACT_PREFIX)$(BUILD_ARTIFACT_NAME)$(if $(BUILD_ARTIFACT_EXTENSION),.$(BUILD_ARTIFACT_EXTENSION),)

# Add inputs and outputs from these tool invocations to the build variables 
SECONDARY_FLASH += \
sense_ics_firmware_sleep.hex \

SECONDARY_SIZE += \
sense_ics_firmware_sleep.siz \


# All Target
all: main-build

# Main-build Target
main-build: sense_ics_firmware_sleep.elf secondary-outputs

# Tool invocations
sense_ics_firmware_sleep.elf: $(OBJS) $(USER_OBJS) makefile objects.mk $(OPTIONAL_TOOL_DEPS)
	@echo 'Building target: $@'
	@echo 'Invoking: GNU ARM Cross C Linker'
	arm-none-eabi-gcc -mcpu=cortex-m3 -mthumb -mlittle-endian -Os -fmessage-length=0 -fsigned-char -ffunction-sections -fdata-sections -Wunused -Wuninitialized -Wall -Wshadow  -g -T "D:\OnSemiV2\sense_ics_firmware_sleep\RTE\Device\RSL10\sections.ld" -nostartfiles -Xlinker --gc-sections -Wl,-Map,"sense_ics_firmware_sleep.map" --specs=nano.specs -u _printf_float --specs=nosys.specs -o "sense_ics_firmware_sleep.elf" -Wl,--start-group $(OBJS) $(USER_OBJS) $(LIBS) -Wl,--end-group
	@echo 'Finished building target: $@'
	@echo ' '

sense_ics_firmware_sleep.hex: sense_ics_firmware_sleep.elf makefile objects.mk $(OPTIONAL_TOOL_DEPS)
	@echo 'Invoking: GNU ARM Cross Create Flash Image'
	arm-none-eabi-objcopy -O ihex "sense_ics_firmware_sleep.elf"  "sense_ics_firmware_sleep.hex"
	@echo 'Finished building: $@'
	@echo ' '

sense_ics_firmware_sleep.siz: sense_ics_firmware_sleep.elf makefile objects.mk $(OPTIONAL_TOOL_DEPS)
	@echo 'Invoking: GNU ARM Cross Print Size'
	arm-none-eabi-size --format=berkeley "sense_ics_firmware_sleep.elf"
	@echo 'Finished building: $@'
	@echo ' '

# Other Targets
clean:
	-$(RM) $(OBJS)$(SECONDARY_FLASH)$(SECONDARY_SIZE)$(ASM_DEPS)$(S_UPPER_DEPS)$(C_DEPS) sense_ics_firmware_sleep.elf
	-@echo ' '

secondary-outputs: $(SECONDARY_FLASH) $(SECONDARY_SIZE)

.PHONY: all clean dependents main-build

-include ../makefile.targets
