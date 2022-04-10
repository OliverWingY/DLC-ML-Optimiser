function [successfull] = BuildAnnModel()
successfull = false;

DepositionTime = [45.12278754;	36.0433069;	45.14334973;	47.50963872;	40.07774017;	37.10612236;	46.11791178;	42.2766028;	50.09629298;	41.1563868];
MicrowavePower = [1032;	1087;	1032;	1087;	1178;	1178;	1178;	1032; 1032; 1087];
WorkingPressure = [0.012;	0.011;	0.011;	0.009;	0.009;	0.011;	0.011;	0.013;	0.011;	0.013];
GasFlowRateRatio = [94;	92;	85;	40;	74;	42;	91;	43;	66;	67];
Hardness = [4.9;	4.1;	3.4;	2.1;	3.9;	2.5;	4.2;	2.2;	3.3; 4.7];

inputMatrix = [DepositionTime, MicrowavePower, WorkingPressure, GasFlowRateRatio].';

netconf = [6, 5];

AnnModel = feedforwardnet(netconf);
AnnModel = train(AnnModel, inputMatrix, Hardness.');

assignin('base', 'AnnModel', AnnModel)
successfull = true;