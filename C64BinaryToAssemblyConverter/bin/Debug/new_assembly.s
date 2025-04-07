                SEI
                JSR label0
                JSR label1
                JSR label2
label3      JMP label3
label4      LDX #$00
branch0      LDA $0AF2,X
                STA $0400,X
                LDA #$01
                STA $D800,X
                LDA #$00
                STA $D900,X
                INX
                BNE branch0
                LDX #$00
branch1      LDA #$9F
                STA $0590,X
                INX
                CPX #$28
                BNE branch1
                RTS
label1      SEI
                LDA #$1E
                STA $D018
                NOP
                NOP
                LDA #$7F
                AND $D011
                STA $D011
                LDA #$00
                STA $D020
                STA $D021
                JSR label4
                CLI
                RTS
label2      SEI
                LDA $DC0D
                LDA #$7F
                STA $DC0D
                LDA #$01
                STA $D019
                STA $D01A
                LDX #$9F
                LDY #$09
                STX $FFFE
                STY $FFFF
                LDX #$9E
                LDY #$09
                STX $FFFA
                STY $FFFB
                CLI
                LDA #$35
                STA $01
                JSR label5
                NOP
                NOP
                LDA #$00
                TAX
                TAY
                RTS
label6      STA $0AD5
                STY $0AD6
                STX $0AD7
                CLC
branch2      LDA $D019
                AND #$01
                BEQ branch2
                STA $D019
                RTS
label8      LDA $0AD5
                LDY $0AD6
                LDX $0AD7
                RTI
                JSR label6
                LDA #$08
                ORA #$C0
                STA $D016
                LDA $0AD8
                STA $D020
                STA $D021
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                LDA #$00
                STA $D020
                STA $D021
                LDA #$30
                STA $D012
                LDX #$DA
                LDY #$09
                STX $FFFE
                STY $FFFF
                CLI
                JSR label7
                JMP label8
                JSR label6
                LDA #$08
                ORA #$C0
                STA $D016
                LDA #$00
                STA $D020
                STA $D021
                LDA #$80
                STA $D012
                LDX #$02
                LDY #$0A
                STX $FFFE
                STY $FFFF
                CLI
                JSR label9
                JMP label8
                JSR label6
                LDA $0AD4
                ORA #$C0
                STA $D016
                LDA $0AD8
                STA $D020
                STA $D021
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                NOP
                LDA #$00
                STA $D020
                STA $D021
                LDA #$8A
                STA $D012
                LDX #$9F
                LDY #$09
                STX $FFFE
                STY $FFFF
                CLI
                JSR label10
                JSR label11
                JMP label8
label11      LDA $0AD4
                SEC
                SBC #$02
                STA $0AD4
                BPL branch3
                AND #$07
                STA $0AD4
                JSR label12
branch3      RTS
label12      LDX #$00
branch4      LDA $0591,X
                STA $0590,X
                INX
                CPX #$27
                BNE branch4
                LDY $0AD3
                LDA $0BFD,Y
                CLC
                ADC #$7F
                STA $05B7
                INC $0AD3
                RTS
label10      LDX #$00
branch6      LDA $D012
branch5      CMP $D012
                BEQ branch5
                LDA $0AE1,X
                STA $D021
                INX
                CPX #$06
                BNE branch6
                RTS
label7      INC $0AD1
                LDX $0AD1
                CPX #$03
                BNE branch7
                LDA #$00
                STA $0AD1
                LDA $0AE1
                PHA
                LDX #$00
branch8      LDA $0AE2,X
                STA $0AE1,X
                INX
                CPX #$07
                BNE branch8
                PLA
                STA $0AE8
                LDY $0AD2
                CPY #$08
                BNE branch9
                LDY #$00
                STY $0AD2
branch9      LDA $0AEA,Y
                STA $0AD8
                INC $0AD2
branch7      RTS
                ORA ($04,X)
label0 = $E544
label5 = $FB1D
label9 = $ED12
