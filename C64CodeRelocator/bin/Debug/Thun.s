.C:0900  78          SEI
.C:0901  20 44 E5    JSR $E544
.C:0904  20 2F 09    JSR $092F
.C:0907  20 4C 09    JSR $094C
.C:090a  4C 0A 09    JMP $090A
.C:090d  A2 00       LDX #$00
.C:090f  BD F2 0A    LDA $0AF2,X
.C:0912  9D 00 04    STA $0400,X
.C:0915  A9 01       LDA #$01
.C:0917  9D 00 D8    STA $D800,X
.C:091a  A9 00       LDA #$00
.C:091c  9D 00 D9    STA $D900,X
.C:091f  E8          INX
.C:0920  D0 ED       BNE $090F
.C:0922  A2 00       LDX #$00
.C:0924  A9 9F       LDA #$9F
.C:0926  9D 90 05    STA $0590,X
.C:0929  E8          INX
.C:092a  E0 28       CPX #$28
.C:092c  D0 F6       BNE $0924
.C:092e  60          RTS
.C:092f  78          SEI
.C:0930  A9 1E       LDA #$1E
.C:0932  8D 18 D0    STA $D018
.C:0935  EA          NOP
.C:0936  EA          NOP
.C:0937  A9 7F       LDA #$7F
.C:0939  2D 11 D0    AND $D011
.C:093c  8D 11 D0    STA $D011
.C:093f  A9 00       LDA #$00
.C:0941  8D 20 D0    STA $D020
.C:0944  8D 21 D0    STA $D021
.C:0947  20 0D 09    JSR $090D
.C:094a  58          CLI
.C:094b  60          RTS
.C:094c  78          SEI
.C:094d  AD 0D DC    LDA $DC0D
.C:0950  A9 7F       LDA #$7F
.C:0952  8D 0D DC    STA $DC0D
.C:0955  A9 01       LDA #$01
.C:0957  8D 19 D0    STA $D019
.C:095a  8D 1A D0    STA $D01A
.C:095d  A2 9F       LDX #$9F
.C:095f  A0 09       LDY #$09
.C:0961  8E FE FF    STX $FFFE
.C:0964  8C FF FF    STY $FFFF
.C:0967  A2 9E       LDX #$9E
.C:0969  A0 09       LDY #$09
.C:096b  8E FA FF    STX $FFFA
.C:096e  8C FB FF    STY $FFFB
.C:0971  58          CLI
.C:0972  A9 35       LDA #$35
.C:0974  85 01       STA $01
.C:0976  20 1D FB    JSR $FB1D
.C:0979  EA          NOP
.C:097a  EA          NOP
.C:097b  A9 00       LDA #$00
.C:097d  AA          TAX
.C:097e  A8          TAY
.C:097f  60          RTS
.C:0980  8D D5 0A    STA $0AD5
.C:0983  8C D6 0A    STY $0AD6
.C:0986  8E D7 0A    STX $0AD7
.C:0989  18          CLC
.C:098a  AD 19 D0    LDA $D019
.C:098d  29 01       AND #$01
.C:098f  F0 F9       BEQ $098A
.C:0991  8D 19 D0    STA $D019
.C:0994  60          RTS
.C:0995  AD D5 0A    LDA $0AD5
.C:0998  AC D6 0A    LDY $0AD6
.C:099b  AE D7 0A    LDX $0AD7
.C:099e  40          RTI
.C:099f  20 80 09    JSR $0980
.C:09a2  A9 08       LDA #$08
.C:09a4  09 C0       ORA #$C0
.C:09a6  8D 16 D0    STA $D016
.C:09a9  AD D8 0A    LDA $0AD8
.C:09ac  8D 20 D0    STA $D020
.C:09af  8D 21 D0    STA $D021
.C:09b2  EA          NOP
.C:09b3  EA          NOP
.C:09b4  EA          NOP
.C:09b5  EA          NOP
.C:09b6  EA          NOP
.C:09b7  EA          NOP
.C:09b8  EA          NOP
.C:09b9  EA          NOP
.C:09ba  EA          NOP
.C:09bb  EA          NOP
.C:09bc  A9 00       LDA #$00
.C:09be  8D 20 D0    STA $D020
.C:09c1  8D 21 D0    STA $D021
.C:09c4  A9 30       LDA #$30
.C:09c6  8D 12 D0    STA $D012
.C:09c9  A2 DA       LDX #$DA
.C:09cb  A0 09       LDY #$09
.C:09cd  8E FE FF    STX $FFFE
.C:09d0  8C FF FF    STY $FFFF
.C:09d3  58          CLI
.C:09d4  20 97 0A    JSR $0A97
.C:09d7  4C 95 09    JMP $0995
.C:09da  20 80 09    JSR $0980
.C:09dd  A9 08       LDA #$08
.C:09df  09 C0       ORA #$C0
.C:09e1  8D 16 D0    STA $D016
.C:09e4  A9 00       LDA #$00
.C:09e6  8D 20 D0    STA $D020
.C:09e9  8D 21 D0    STA $D021
.C:09ec  A9 80       LDA #$80
.C:09ee  8D 12 D0    STA $D012
.C:09f1  A2 02       LDX #$02
.C:09f3  A0 0A       LDY #$0A
.C:09f5  8E FE FF    STX $FFFE
.C:09f8  8C FF FF    STY $FFFF
.C:09fb  58          CLI
.C:09fc  20 12 ED    JSR $ED12
.C:09ff  4C 95 09    JMP $0995
.C:0a02  20 80 09    JSR $0980
.C:0a05  AD D4 0A    LDA $0AD4
.C:0a08  09 C0       ORA #$C0
.C:0a0a  8D 16 D0    STA $D016
.C:0a0d  AD D8 0A    LDA $0AD8
.C:0a10  8D 20 D0    STA $D020
.C:0a13  8D 21 D0    STA $D021
.C:0a16  EA          NOP
.C:0a17  EA          NOP
.C:0a18  EA          NOP
.C:0a19  EA          NOP
.C:0a1a  EA          NOP
.C:0a1b  EA          NOP
.C:0a1c  EA          NOP
.C:0a1d  EA          NOP
.C:0a1e  EA          NOP
.C:0a1f  EA          NOP
.C:0a20  EA          NOP
.C:0a21  EA          NOP
.C:0a22  EA          NOP
.C:0a23  EA          NOP
.C:0a24  EA          NOP
.C:0a25  EA          NOP
.C:0a26  EA          NOP
.C:0a27  EA          NOP
.C:0a28  EA          NOP
.C:0a29  EA          NOP
.C:0a2a  EA          NOP
.C:0a2b  EA          NOP
.C:0a2c  EA          NOP
.C:0a2d  EA          NOP
.C:0a2e  EA          NOP
.C:0a2f  A9 00       LDA #$00
.C:0a31  8D 20 D0    STA $D020
.C:0a34  8D 21 D0    STA $D021
.C:0a37  A9 8A       LDA #$8A
.C:0a39  8D 12 D0    STA $D012
.C:0a3c  A2 9F       LDX #$9F
.C:0a3e  A0 09       LDY #$09
.C:0a40  8E FE FF    STX $FFFE
.C:0a43  8C FF FF    STY $FFFF
.C:0a46  58          CLI
.C:0a47  20 81 0A    JSR $0A81
.C:0a4a  20 50 0A    JSR $0A50
.C:0a4d  4C 95 09    JMP $0995
.C:0a50  AD D4 0A    LDA $0AD4
.C:0a53  38          SEC
.C:0a54  E9 02       SBC #$02
.C:0a56  8D D4 0A    STA $0AD4
.C:0a59  10 08       BPL $0A63
.C:0a5b  29 07       AND #$07
.C:0a5d  8D D4 0A    STA $0AD4
.C:0a60  20 64 0A    JSR $0A64
.C:0a63  60          RTS
.C:0a64  A2 00       LDX #$00
.C:0a66  BD 91 05    LDA $0591,X
.C:0a69  9D 90 05    STA $0590,X
.C:0a6c  E8          INX
.C:0a6d  E0 27       CPX #$27
.C:0a6f  D0 F5       BNE $0A66
.C:0a71  AC D3 0A    LDY $0AD3
.C:0a74  B9 FD 0B    LDA $0BFD,Y
.C:0a77  18          CLC
.C:0a78  69 7F       ADC #$7F
.C:0a7a  8D B7 05    STA $05B7
.C:0a7d  EE D3 0A    INC $0AD3
.C:0a80  60          RTS
.C:0a81  A2 00       LDX #$00
.C:0a83  AD 12 D0    LDA $D012
.C:0a86  CD 12 D0    CMP $D012
.C:0a89  F0 FB       BEQ $0A86
.C:0a8b  BD E1 0A    LDA $0AE1,X
.C:0a8e  8D 21 D0    STA $D021
.C:0a91  E8          INX
.C:0a92  E0 06       CPX #$06
.C:0a94  D0 ED       BNE $0A83
.C:0a96  60          RTS
.C:0a97  EE D1 0A    INC $0AD1
.C:0a9a  AE D1 0A    LDX $0AD1
.C:0a9d  E0 03       CPX #$03
.C:0a9f  D0 2F       BNE $0AD0
.C:0aa1  A9 00       LDA #$00
.C:0aa3  8D D1 0A    STA $0AD1
.C:0aa6  AD E1 0A    LDA $0AE1
.C:0aa9  48          PHA
.C:0aaa  A2 00       LDX #$00
.C:0aac  BD E2 0A    LDA $0AE2,X
.C:0aaf  9D E1 0A    STA $0AE1,X
.C:0ab2  E8          INX
.C:0ab3  E0 07       CPX #$07
.C:0ab5  D0 F5       BNE $0AAC
.C:0ab7  68          PLA
.C:0ab8  8D E8 0A    STA $0AE8
.C:0abb  AC D2 0A    LDY $0AD2
.C:0abe  C0 08       CPY #$08
.C:0ac0  D0 05       BNE $0AC7
.C:0ac2  A0 00       LDY #$00
.C:0ac4  8C D2 0A    STY $0AD2
.C:0ac7  B9 EA 0A    LDA $0AEA,Y
.C:0aca  8D D8 0A    STA $0AD8
.C:0acd  EE D2 0A    INC $0AD2
.C:0ad0  60          RTS
.C:0ad1  01 04       ORA ($04,X)
.C:0ad3  1A          NOOP
.C:0ad4  00          BRK
.C:0ad5  9F 09 06    SHA $0609,Y
.C:0ad8  03 00       SLO ($00,X)
.C:0ada  00          BRK
.C:0adb  00          BRK
.C:0adc  00          BRK
.C:0add  00          BRK
.C:0ade  00          BRK
.C:0adf  00          BRK
.C:0ae0  00          BRK   ; Text !! - 
.C:0ae1  0F 0C 0B    SLO $0B0C
.C:0ae4  00          BRK
.C:0ae5  0B 0C       ANC #$0C
.C:0ae7  0F 01 00    SLO $0001
.C:0aea  00          BRK
.C:0aeb  06 0E       ASL $0E
.C:0aed  03 01       SLO ($01,X)
.C:0aef  03 0E       SLO ($0E,X)
.C:0af1  06 2D       ASL $2D
.C:0af3  20 14 08    JSR $0814
.C:0af6  05 20       ORA $20
.C:0af8  17 09       SLO $09,X
.C:0afa  0C 04 20    NOOP $2004
.C:0afd  13 14       SLO ($14),Y
.C:0aff  19 0C 05    ORA $050C,Y

