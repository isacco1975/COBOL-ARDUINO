       IDENTIFICATION DIVISION.
       PROGRAM-ID.    "TS",  is initial.
       AUTHOR.        Isaac Garcia Peveri.
       REMARKS.       Written in AcuCobol 7.0.0
      *LAST-EDIT.     2024, SEPTEMBER 12.
      /
      ******************************************************************
      * A telesketch (etch a sketch) written in Cobol                  *
      * CHARACTER BASED VERSION                                        *
      ******************************************************************
      *
       WORKING-STORAGE SECTION.
       77 KEY-PRESSED PIC X.
       77 SOCKET-HANDLE  USAGE HANDLE.
      *   Arguments decoded by ARDUINO
       77 CMD-ARGS       PIC X(9)       VALUE SPACES.
       77 X-VAL          PIC 9(4)       VALUE ZERO.
       77 Y-VAL          PIC 9(4)       VALUE ZERO.
       77 X-COL          PIC 9(4)       VALUE ZERO.
       77 Y-COL          PIC 9(4)       VALUE ZERO.
      /
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       PROCEDURE DIVISION.
       MAIN.
            PERFORM INIT-AREA

            CALL 'C$SOCKET'         USING 3
                                          64000
                                          "127.0.0.1"
                                    GIVING SOCKET-HANDLE

            PERFORM WORKING-CYCLE

            STOP RUN
            .
      /
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       INIT-AREA.
            DISPLAY WINDOW ERASE
            DISPLAY ' '
            DISPLAY '                            COBOL Telesketch 1.0'
                    COLOR 3 HIGHLIGHT
            DISPLAY '                             IGP TECH BLOG 2024'
                    COLOR 3 HIGHLIGHT
            .
      /
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       WORKING-CYCLE.
            PERFORM UNTIL KEY-PRESSED = 'X' OR 'x'
               CALL "C$SLEEP"       USING 0.01

               CALL 'C$SOCKET'      USING 6
                                    SOCKET-HANDLE
                                    CMD-ARGS
                                    9

               UNSTRING CMD-ARGS DELIMITED BY ';'
                   INTO X-VAL, Y-VAL

               DISPLAY 'X: '  AT 0431 COLOR 4 HIGHLIGHT
               DISPLAY 'Y: '  AT 0440 COLOR 4 HIGHLIGHT
               DISPLAY X-VAL  AT 0434 COLOR 7 HIGHLIGHT
               DISPLAY Y-VAL  AT 0443 COLOR 7 HIGHLIGHT

               COMPUTE X-COL = X-VAL / 10
               COMPUTE Y-COL = Y-VAL / 15

               DISPLAY 'C: '  AT 0531 COLOR 4 HIGHLIGHT
               DISPLAY 'R: '  AT 0540 COLOR 4 HIGHLIGHT
               DISPLAY X-COL  AT 0534 COLOR 7 HIGHLIGHT
               DISPLAY Y-COL  AT 0543 COLOR 7 HIGHLIGHT

               IF Y-COL < 6
                  MOVE 6 TO Y-COL
               END-IF

               IF Y-COL > 25
                  MOVE 25 TO Y-COL
               END-IF

               IF CMD-ARGS(1:1) = 'C'
                  PERFORM INIT-AREA
               END-IF

               DISPLAY '#'    AT LINE Y-COL, COLUMN X-COL
            END-PERFORM
            .
      /
       END-PROGRAM. "TS".