   M S B A M L   `   `   `  ����>  :Med, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MedMed  5   	       Med.Page_Doctor   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:Med     5   	     � Title$  Page_Doctor��5   	   $	��450��6   $	��800��6*   .��5	      � ���	#FF4FC7FF.+�5
   
   � $
��Left=�6   $��
21,22,0,50q�6*   $	��280��6>    � RowDefinitions 5      �� 5      $	��29* �6    �� 5      $	��32* �6    �� 5      $	��33* �6    �� 5      $	��35* �6    �� 5      $	��34* �6    �� 5      $
��185* �6     � ColumnDefinitions 5      �� 5      $	��12* �6#   �� 5      $	��23* �6#   .+�5      �� $
��Left=�6   $��10,0,0,0q�63   $
��Wrap=�6E   $��Фамилия:��6Y   $	��Top=�6i   $��18(�6�   
�������$��86��6�   $��24��6�   �� 5      $
��Left=�6   $��10,0,0,0q�63   $
��Wrap=�6E   $��E-mail:��6Y   $	��Top=�6h   $��18(�6�   
�������$��4��6�   $��86��6�   $��24��6�   �� 5      $
��Left=�6   $��10,1,0,0q�63   $
��Wrap=�6E   $��Телефон:��6Y   $	��Top=�6i   $��18(�6�   
�������$��3��6�   $��86��6�   $��24��6�   �� 5      $
��Left=�6   $��10,0,0,0q�63   $
��Wrap=�6E   $��Отчество:��6Y   $	��Top=�6j   $��18(�6�   
�������$��2��6�   $��86��6�   $��24��6�   �� 5      $
��Left=�6   $
��Wrap=�63   $��Имя:��6G   $	��Top=�6S   $��18(�6k   
�������$��	10,28,0,0q�6�   $��2��6�   $��86��6�   $��24��6�   �� 5      -   5         ��Name$ Family��5      $��1��6(   $
��Left=�68   $��5,0,0,0q�6S   $
��Wrap=�6d   $�� ��6x   $	��Top=�6�   
�������$��16(�6�   $
��Bold"�6�   $��21��6�   $	��169��6�   �� 5      -   5        $
 Name��5      $��1��6&   $
��Left=�66   $��5,0,0,0q�6Q   $
��Wrap=�6b   $�� ��6v   $	��Top=�6~   
�������$��16(�6�   $
��Bold"�6�   $��1��6�   $��21��6�   $	��169��6�   �� 5      -   5        $ 
MiddleName��5      $��1��6,   $
��Left=�6<   $��5,0,0,0q�6W   $
��Wrap=�6h   $�� ��6|   $	��Top=�6�   
�������$��16(�6�   $
��Bold"�6�   $��2��6�   $��21��6�   $	��169��6�   �� 5      -   5        $ 	Telephone��5      $��1��6+   $
��Left=�6;   $
��Wrap=�6V   $	��---��6j   $	��Top=�6u   
�������$��16(�6�   $
��Bold"�6�   $��3��6�   $��5,0,0,0q�6�   $��21��6�   $	��169��6�   �� 5       -   5        $
 Mail��5       $��1��6&   $
��Left=�66   $
��Wrap=�6Q   $	��---��6e   $	��Top=�6p   
�������$��16(�6�   $
��Bold"�6�   $��4��6�   $��5,0,0,0q�6�   $��21��6�   $	��169��6�   �� 5!      -   5         ��Name$ bChange��5!      $'��!Изменить контакты��65   $
��Left=�6Q   $��	10,10,0,0q�6l   $��5��6   $	��Top=�6�   $	��129��6�   ���	#FFFFFEFE$��2��6�   �� 5"      -   5        $ bSave��5"      $��Сохранить��61   $
��Left=�6E   $��	10,10,0,0q�6`   $��5��6s   $	��Top=�6�   $��75��6�   ���	#FFFFFEFE$��1��6�   $y�Hidden=�6�   �� 5#      -   5        $ bCancel_Change��5#      $��Отмена��6C   $
��Left=�6T   $��	10,10,0,0q�6o   $��5��6�   $	��Top=�6�   $��75��6�   ���	#FFFFFEFE$y�Hidden=�6�   �� 5$      -	   5         ��Name$ boxTelephone��5$      $��1��6,   $
��Left=�6<   $��23��6W   $��5,1,0,0q�6c    �� TextWrapping$
 Wrap=�6t   $�� ��6�   $	��Top=�6�   $	��169��6�   $��16(�6�   $��3��6�   $y�Hidden=�6�   �� 5%      -
   5        $ boxMail��5%      $��1��6'   $
��Left=�67   $��23��6R   $��5,0,0,0q�6^   $
 Wrap=�6o   $�� ��6�   $	��Top=�6�   $	��169��6�   $��16(�6�   $��4��6�   $y�Hidden=�6�   �� 5&      $
��Left=�6   $��	10,43,0,0q�63   $
��Wrap=�6F   $��Должность:��6Z   $	��Top=�6l   $��18(�6�   
�������$��5��6�   $	��106��6�   $��24��6�   $��2��6�   �� 5'      $
��Left=�6   $��	10,72,0,0q�63   $
��Wrap=�6F   $��Квалификация:��6Z   $	��Top=�6o   $��18(�6�   
�������$��5��6�   $	��129��6�   $��24��6�   $��2��6�   �� 5(      -   5        $ Position��5(      $��1��6*   $
��Left=�6:   $��25,46,-10,0q�6U   $
��Wrap=�6j   $�� ��6~   $	��Top=�6�   
�������$��16(�6�   $
��Bold"�6�   $��5��6�   $��21��6�   $	��169��6�   �� 5)      -   5        $ Qualification��5)      $��1��6/   $
��Left=�6?   $��48,75,-33,0q�6Z   $
��Wrap=�6o   $�� ��6�   $	��Top=�6�   
�������$��16(�6�   $
��Bold"�6�   $��5��6�   $��21��6�   $	��169��6�   5*      �� 5+   
   -   5        $ 
Receptions��5+      $
��Left=�6(   $��
414,51,0,0q�6C   $
��Wrap=�6W   $*��$Назначенные приёмы:��6k   $	��Top=�6�   
�������$��14(�6�   $	��255��6�   �� 5,   
   -   5         ��Name$ Actual_Receptions��5,      $
��Left=�6.   $	��264��6I   $��
414,75,0,0q�6V   $	��Top=�6j   $	��255��6�   $ �� IsSynchronizedWithCurrentItem$ FalseY�6�   5-      5.      