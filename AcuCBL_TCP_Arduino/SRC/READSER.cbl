       IDENTIFICATION DIVISION.
       PROGRAM-ID.    "READSER",  is initial.
       AUTHOR.        Isaac Garcia Peveri.
       REMARKS.       Written in AcuCobol 7.0.0
      ******************************************************************
      ******************************************************************
      *
       WORKING-STORAGE SECTION.             
       77 FORM1-HANDLE USAGE HANDLE OF WINDOW.
              
       SCREEN SECTION.
       01 FORM1.
          03 LABEL 'Esempio di programma grafico in cobol' LINE 2 COL 3.
          03 E1  ENTRY-FIELD LINE 4 COL 3 VALUE 'XYZ'.
          03 E2  ENTRY-FIELD LINE 4 COL 9 VALUE 'ABC'.
          03 PB1 PUSH-BUTTON LINE 6 COL 3 CANCEL-BUTTON.	
      *
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       PROCEDURE DIVISION.
       MAIN.
            DISPLAY INITIAL GRAPHICAL WINDOW
                    COLOR 65793
                    TITLE "IGP TEST"
                    SYSTEM MENU
                    HANDLE IN FORM1-HANDLE
            
            DISPLAY FORM1 UPON FORM1-HANDLE
                   
            PERFORM UNTIL 1 = 2
               ACCEPT FORM1 ON EXCEPTION CONTINUE END-ACCEPT
            END-PERFORM       
                   
            STOP RUN
            .

      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*