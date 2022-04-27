function [successfullInitialisation] = InitialiseMatlabWorkspace()
successfullInitialisation = false;
evalin('base','clear');
assignin('base', 'DepositionTime', [45.12278754;	36.0433069;	45.14334973;	47.50963872;	40.07774017;	37.10612236;	46.11791178;	42.2766028;	50.09629298;	41.1563868]);
assignin('base', 'MicrowavePower', [1032;	1087;	1032;	1087;	1178;	1178;	1178;	1032; 1032; 1087]);
assignin('base', 'WorkingPressure',[0.012;	0.011;	0.009;	0.009;	0.009;	0.011;	0.013;	0.013;	0.011;	0.013]);
assignin('base', 'GasFlowRateRatio', [100;	100;	100;	10;	73.9;	10;	100;	10;	55;	55]);
assignin('base', 'Hardness', [4.9;	4.1;	3.4;	2.1;	3.9;	2.5;	4.2;	2.2;	3.3; 4.7]);

successfullInitialisation = true;