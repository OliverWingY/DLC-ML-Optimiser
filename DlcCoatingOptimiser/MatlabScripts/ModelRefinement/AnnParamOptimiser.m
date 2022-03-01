
clear;
DepositionTime = [45.12278754;	36.0433069;	45.14334973;	47.50963872;	40.07774017;	37.10612236;	46.11791178;	42.2766028;	50.09629298;	41.1563868];
MicrowavePower = [900;	1050;	900;	1050;	1200;	1200;	1200;	900;	900;	1050];
WorkingPressure = [0.013;	0.011;	0.009;	0.009;	0.009;	0.011;	0.013;	0.013;	0.011;	0.013];
GasFlowRateRatio = [100;	100;	100;	10;	73.9;	10;	100;	10;	55;	55];
Hardness = [4.9;	4.1;	3.4;	2.1;	3.9;	2.5;	4.2;	2.2;	3.3; 4.7];

inputMatrix = [DepositionTime, MicrowavePower, WorkingPressure, GasFlowRateRatio].';

inputTrainingParams = readmatrix("AnnVariationStudy.csv");
outputTrainingParams = zeros(81,2);
for c = 1:81
rng(0);
clear AnnModel;
netconf = [inputTrainingParams(c,2), inputTrainingParams(c,3)];


AnnModel = feedforwardnet(netconf, 'traingd');

AnnModel.trainParam.lr = inputTrainingParams(c,4);
AnnModel.trainParam.mc = inputTrainingParams(c,5);
[AnnModel, tr] = train(AnnModel, inputMatrix, Hardness.');
outputTrainingParams(c,1) = inputTrainingParams(c,1);
outputTrainingParams(c,2) = tr.best_tperf;

clear tr;
writematrix(outputTrainingParams, 'Results.csv')
end

