﻿//  Copyright (c) 2021 Demerzel Solutions Limited
//  This file is part of the Nethermind library.
// 
//  The Nethermind library is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  The Nethermind library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.

using Nethermind.Core.Crypto;
using Nethermind.Core.Extensions;
using Nethermind.Core.Test.Builders;
using Nethermind.Network.P2P.Subprotocols.Eth.V63;
using Nethermind.Network.P2P.Subprotocols.Eth.V63.Messages;
using NUnit.Framework;

namespace Nethermind.Network.Test.P2P.Subprotocols.Eth.V63
{
    [Parallelizable(ParallelScope.All)]
    public class GetReceiptsMessageSerializerTests
    {
        private static void Test(Keccak[] keys)
        {
            GetReceiptsMessage message = new GetReceiptsMessage(keys);
            GetReceiptsMessageSerializer serializer = new GetReceiptsMessageSerializer();
            var serialized = serializer.Serialize(message);
            GetReceiptsMessage deserialized = serializer.Deserialize(serialized);

            Assert.AreEqual(keys.Length, deserialized.Hashes.Count, "count");
            for (int i = 0; i < keys.Length; i++) Assert.AreEqual(keys[i], deserialized.Hashes[i], $"blockHashes[{i}]");
        }

        [Test]
        public void Roundtrip()
        {
            Keccak[] hashes = {TestItem.KeccakA, TestItem.KeccakB, TestItem.KeccakC};
            Test(hashes);
        }

        [Test]
        public void Roundtrip_with_nulls()
        {
            Keccak[] hashes = {null, TestItem.KeccakA, null, TestItem.KeccakB, null, null};
            Test(hashes);
        }
        
        [Test]
        public void Roundtrip_example_from_network()
        { 
            byte[] bytes1 = Bytes.FromHexString("f8e7a0ccc8a764fbb24529fdb7d0b6144f4aa19fa5e8a52921b61e649312bdbcc5baf3a02f81d8850c9d554207de6e9f2ea17569f3ffac052c759c76a8619b611efad30aa0f9bc1af769997845815e4fac152fcb9b9544d806add15452b5ec45289512747ea026b807339bb8991ce39f8eb28417781d3984e9f9171dc954764f2a2a1a94fda8a0bd7a5373abf1c92e7445957612ab79aa3bd23cd463a4f8b2e9bc244fa2de5e3ba0d32cef2e48985fbb9973bd24038ec4cbe8afe83526509d67197c01c72208acbda03587a5c6e884b0c282521ed333cca786abcbba5e55328613ff5092559e66bc56");
            byte[] bytes2 = Bytes.FromHexString("f915eaa01229fa33a97d742c1e615724c40f513f60ea39e27a795dcd24839f56d3be9a13a0857392d4d502cc8a9118b7452ba29f7d4c905db2361d030445578f007c27b4bda04fea11b1e98c297da94e8f995b05a0fda0bad3336c394d8ef79c7712319180daa0d51ad42c6f098762c327ec3466f4cc564b01d4866fe9d9170321365fa273c69ea01a46f59abe35e5e12b3820671f645ecadeb83a1db3e295edfe52ef4712428946a06016b2db2ee7447042e62ec01b02451228f6d44858b63916f6eb2843ea943964a04affaa0a06b45e236a6d3191af95a24007d57c2847eaae5fe614fabaf5d346a2a0aa91cd3117861451be3abe2238b6b70d96fea61a24ea0db480e187add7d735d1a0f71751de72e1f116adebdaa75080fa6fba43ab340b186bf9d473cebdcd97c2a9a04e2087c1878bd05692d11eeee0470d0e3cff053ba920e60986d3fc4f64808d1da05ab964a4673be19c129bdbb6316ad224b01ada36b2b78893d33e924b2a53861ba04ed3216cf786ee20492d2c95e3a8343eee05e4253730bf94651748ae0fbb8fc3a0599936c1c4920e2a0a668b65c7e89e42fab8b3d27682f68a6912bf13d60e566ba0df4d49b6c9182a337762f132ba34119cd413d8bc65828b4db9454b52f8fc05ffa04d6932f351fcdbe03b29d21c7fffe58f79ff14ebace8cbf0c9d72d537e945ad1a058c289da71b106d5560d01e3f042154f78bae64a5338c1617b8bb29a2909f49ca03cc3fc7292e255aa7b80d70ad59385fa2c3da79e8f1d7546cc6bc84756a41f23a0172fd6bbf65332701d145aeb5d34e94f73098641553102e3f80b6d52b83f3a7aa00b5242c897e3df0f78e7c486f7d441a91458ffbeb5e0539d7fb136df3942d688a036b807cf24ed5984fe11abd0bebd15513b70c9852179bd6eae696ed3b59d8784a0cb01b00436c9ee312109a2bca88dac5e6b18f9f19f64306eeb896af223add506a0d9790eba586859a7818e0100ede30289051713d4281035d1fcf1d2d56e9d9c2ea001e9764c14eb9bf4c31fa921dc6890fcc75fcfbf3de8ed3bf0ae74be7cbe1ff0a0da364bb85d3eb86b9de58a489070dc6d1e861520c67710d73ed07772429edf96a0774030864c28e5da4fc0094dbbb2ec7a829a66aad960c400f8c6b0d0b2cadf11a0093b5cae7d2c0d83e6fe2462dbab79979543282099477322d6fd80753f056196a03ed48fc99ef34ae7378855b5f18942ec948d41e6ef40b5c6f3e3b644ab693144a0bee35269787c0ea420a69c4d4f199c67b1c64314b3a562953e8d7e3613bfeca1a073831f947d35e669a2927d323dabca00bf188311c104d0153a89917c2cb2f3ffa0ba97a7d2aeac6f48133a890ad31c0a52f7f1cb594c598e1018978dd5ccda159ba0f0fb585de86292208600b3ffe4143e31fc924ddc3e76c904dd32c649c007c438a0c87cdf5fef39638b2b4ba19d630193425c5f2eba218d009c0ce114312d5b5e9ba0c978ac0810be600ad9aa9f61f3d12c7dbed7a5c46f8f98003f64da506f923fc9a0618c00727b5da80459ae406c3956c71da2ae5de91bfe78ca3efcc12db4bbde38a051e22e5256b09c38fad04b3549ce44e416c72601a8a6c21beef3214a5ecdd19da0ce42504adf5b30ba401abb407b44dabfb1394971200bcc0bec6988a20ec5e300a069e577b286224f9e8c4d83c6375fb624dfe2e383037ef2ff5e44ec8d1daf1149a09b59d343b75fc98f2b704c03af2dada8e5b56991d47ec2396e51d6856417b4d8a00f4ec64accaec7e4fbc85856ce835a9250493e076c6d9f7dd95c525cd5ee80dba06f3c7b46ea857217493448dda8ba2186ab2217f6eb1b15b6efbccc1721ed0da8a070e2d704990c7e46f15cda0d890ae0d39d992da2a55ce5881e6a70f2eff557cca0fdbe44bda6022955d036c85bbe5d27e19d96dc8343ecb546298356ad00530ac0a0ef88e60ccdf5276e3c5a33be79e6b0bf0b93880c60783da47cb78f47b644fe52a0efe4c876eed5b0eab0debcb74653cde1c31e1fd9a24c8a15d66f359a725663d6a024f46d7b02dc043a3d9213985eab013e320d2506015392b32c28806f888cd9a1a0f8bd63dd263161795e824a9fb8c852e24c5985e0935258c470397509d2a1c9e3a0718a3ad8ebba19d2d829b7dc8bfa832568c0a341be0d6503470f632f7be7b51ca0f7e67712e5156e1a19558a171acc0a5ff37874787764fbe116d174ab21ea00eaa0f0994f8cde57dba789aa56cdd6915c19e1d132417746e8a6b59e4d875b95d19ea0a01c9c725a53ede31ad26514aeee79e1bae9593ad7c87c4e23f8a2512ae588f1a047e3d24f87dc2e7f37c26f05e3878f3e2af73a5710249e105d260fdfbda2c851a0130b078704dba2f66c801e6a0f22fe891b44e99b8991c377cb2dc61557e50e9da01a49007c6df122624ff7170c4d4e24eb43225f2aaa0689b70eafd9511733e63ba002875f177d96b6eaf9bef14a76ad9689bd8ba9cc559eb65537aa33e54d53df9ea0d05321bd6dd31ba11c2a48a0384bf8295bef90e64c4c5115a218ba46127af7cea078bf2f509488b1c2f2a0d2a2d56c7773f98775366e57447dda93e71b4ead0c84a0ce8e18df2dcfe7fec3ece8efc2ded6628557b19c1ed8fd2ca602753c9789fb40a09024836df930e6390b15ddfe059d9bd900c7214829211fa1f5c7863896918813a05848676dd3c13249de6215634a81e24adbbeee31cc0c523c5d41b432c17446dca03bda01ae26401868867123ed69ba73248518ed325fe57ffbc259bdb859e15466a0cf5263f39c208d84dc743049b2567308b7ed373f30ef62c78ed2f2a268dc845ea052cd9e8600aec5bf095d59d21025d2ded5f590f09a75f96287e7dc988c4e4cbea0f1ccd4813a9bd37f53f5f48fc8da5734222bc7d831924640d9bb291d7b3c1d45a05e77e2c52e13e91c30aecad44c2e5bc20f9edd51060abe14a68f918bd09e212ba0b11eed16aad027f78bdcf30622a448aa1d6e220d4749b85997df54b17e877356a03d8830855bb9f61dc9836c9a491c65ea822a2ab788f24ec6f30836c2b9823d86a054770b492378b8bb7c32e36912a3245ad98d07039881b2718f40d0fbe9aa1fcfa02b2ac09bfd841b913a1ff3c84b308899eeb4281cbbcf2a90b8cb3db5c9a5f868a0f339268aa265f7cdf622457e6df14e3d03067be4bc91ea1fce8d00a17b87b710a07c0f186ff2159c1e812f28b7111eae40558660c89978285c47f13b658473d111a016ed5d95a2664d0ffe9a19f16963afbe888ae152e65d32b55bc3145cd94c26dba036519ec5b89fc5095aaa18971dcdf15bedfb202f824616e590061cfce2486618a04710657689ad3e97d3479ea8699c02e0a82d6f97984f87a1abd445f0ac270fafa0f949a44e691ba835dc38dfaaebdc395bfa17a033d8717bb8ba2fdafee55f7584a05f625d2429887145da94c5694c87e7b72b4f1a18d0f4e40943c356b2fd67fd5ba0d83126ad81f54a05d5329f33cc366396fcd4002a24da7948aa12f8be7669a50fa066de5d8666b1df367253f95b2d334543232b1d3ae0c3a937fe6f9d5b47b5488aa09860cfabfef96e808fe4fbbea26f764b6d8025d875100a5f51ce08fbba9e9f9ba0121c0dfce06a1e0b56f81aa25e2c18bab56a61223a9a878e5bd01f2a745c036fa0a7d43a9d25ebf74be5e47dfe18383b13ea17e333792e5b44b3c0a9714aaa9996a0397371bda23b93ab45d50b5bc4197de0cf66e05b505045ef91e616b65f366effa0bcbab28afe3ad21a4a7451d18151c2c6a09e4723eec794ebbdb24e7888fbe122a041f21e7a287e5cc3697c8a63b611b9a2a9cf86cdc8605ae774c4439cb90ae56fa0b4bb4cf76ebb5ed494e00b69011d33945469577a195b5c1d576ddcca9a30338aa00592fe1efda0b2d34cb2b3b56f8076001c7a52ed8fe482ba97ab9b30cbea03d0a0823398e84aaadd4c70c823ea7f5d75516eb37ad716fd88a8556aa98b13068fada0360263ae32fca514d9722fa3c18dd6967dda04ce7da5b4f2cdbfe3efe171972da095902685b022929a93ae4751ed1e3316adcfaa7834fedfd1651d9d5002387d07a043c8bdf97e58bb92ddb7c6791bcf4a9798d08bda751d70ed5bfdf8e3fee9e1f2a09c27eb4f14ee13fba7642359ced3b99e115a54d3dba984e7ad51d0014c83b5aca0a7c04446744d7af98722212a1fded148f26bb922c4e70d93a6d646c3bf82fbe4a079c0e01655dad5a685fb394a444e28140027fb8bf8462a0487dd6b65f5cbbca7a0fda2505e3d6b863ac9b93f24760cedfacea0173afeff39c4312d97786c1d0e0ca028a0dc963f827cd19a7f3befc43e6e632a7c940a9743c975329119893479f239a097835925d8a0eee39ccb0c33c15fbf8ea808d41a0f9046454829512e78a160e1a0049b78faa89af0460ac0cfbe3f4c8730b00fa9eac0729d829b19008db912593da06b8ade196ae076454960327938e1ccccb0f0c6670e1d0ccda7d53d9052186eada0ffe7efdfb0cd4d9f38d4b276ad5dce726ffb78c4d463b7e5bf3666d5384a8f5ba053ef3baa4e0fbed66dadb5ada172316722de5dab2e9098d481cdc31467ae6f86a0c9c76f3e9933034f9f13e45fb945334746ad233e46e01c8a823cb904a331f1cda07f43c2289cd6a92aa49d42843075852a2897080720b4036efe531ed3c93a4843a0bdb11a90ebbeb9e30c51f07b7d044d0c2a4d0e5f89b70866e402e5f90b965bbea00c76c03d93eb5b64b943a788c545b74ceb524711cc7d5e938994a6195e1eef61a0698ddc0937264738e4e3b87ec69debedc7fe7e1c84104ae513696fc9e35eed10a0961cd0ff7078660d56d82df8ad26dbcd358547f2bb15611b37686b54e79875f9a0fe00212d3762e48178f1022eca8cd3efbe85ae345f0331e3372241f8773449c4a0cbd46fae972d69073a833008acda28b73c8b5558c8f41867ec75aa0cd625ba07a067eb2b1565c3e408389f2440706b36de257611c9be5e599b361df6b7b0af0c20a00e9fc6ba5c8d461eb828e3fc35c397f87939ab353068b5c1be077e1e83ad5ae0a0e919512c84e1b8893fc74218348e8016d8a8d50ffe43abe6d824b2e2ad2b7303a0217403faa3a705f37b0cc0debaf63bbf37bbbb72cc585d02afd373985e25778ba06a8e37dc29f8a1944e83809355352f48bcb51bc10f27b95e6de401ec95eaadb9a04a0c0a7bff69075785eed199f627e69ae676d995bf2c1d21745d1b9bedb27c49a0ee4c8001dad4ac9186a22860ec686615da7d42ec927b42fe5e8652376b45edc5a02a23f95c7f35bb715b72c717c2681c06de8efcd2630188e5e5a9d7dc009b2ad2a0b2e5e33d9ccc6ad7d1ed3062913f0458f006165459473fe103e2c011593a45e0a0de4c8ad18af9b30a2ea29d4575e64418f1d92b729fa903299cb44ca3c00a9ad7a0216c974384cd739e94d35f4aa772fecce86a359455d069ab9e2f83a0fcf83caaa0c73e07ba8c63d47e6862f185fa4a67c51b983a964b7a26ca09fcedc43a5f3300a07bfed4f406333337e79aab1f450dd7791335998786e570f89a53491020ea969ea01777788814308c866f292c956d0a9b8ab9e26de16cc5dc66c024830c634e29bda0dd405457c6f05f904dbf0692aac5f5e169be0c4ebc6f95ed0c2778f8b4994cfea0d37f6d15fecbcd6afac934d268ecd43e6bc0c36568787087552a9ef87355c96fa0b54923532e09b6c202aad9f0b1712d7fd4d402a4665749c3d961634aa6e157a6a0b4318c18b0b5ca17657d0f3e8b20cb11a2a83ce94e0a89092968eb1048dd9dd3a03ae21e607bb4a7f728d096fc49b3635cf48558581c9e82f848539ea9fdbf3766a0d34c2ad098b4be880e141ed735bc6d199bd6d6ffabea98207521dfe684279048a0164a5a3b55cc32fe8c1ce90214595afd8d2d622ecd87867fd0408cc379ac1137a08c38f9e896fbbedd95d323d8d14bfc4a41823a821153099e35785d9b9fb336d9a046f4625aca2f17f53a7ea4c97fd58fef1ae7d4efa5dfcfd0e3f294bce7c76a0aa081027fa5e399ad4321aa84ac724d5a1089a13587329961723ac20da4f8a326f0a025f4bb09f378d6c75bfd5eeaeae83793982ee104ffd0b4492347c7e53e1ec938a057d67a0ab6e24483eb3b5c36f50f8dafa36ecc4ca007ae69d33886bee306c8d4a051795a18dc74b519aeda2ff81a87c48f00dccb5ac2ca59c95d45e6907a0df6e4a0a531e72bddd58502b7f0786326d4cadbcf34d3af10ade378f95aa6345e2caf85a0c509131d713b9f721c555dd6bdd693f50651da00ff9cd8ae23a6ecac3bb7d4e2a0361337bf25d87677536370b18b537a4a11d9cadac44dca14e23ae67338e6ca5fa0aced12be9c00535521002314777a167b660c557e1de7e52b92eb8ee355d9eecaa02440af333d6cdf9166bae0eafa050ca6eab1a6553d9b633d5543368582cf9561a0afa5c18f87e451bf0c68ffcc5bf1d342e04cd2d1f88fea173b32741333c6998ba0786c55501922eddafb22a5be7004cd6449854c222fa43dd83529b32ac95198f0a0c884f72e6c53e416603b9a9e957a6583dd96abbebb1b7affe2b98275637ab49da0e3206f49ad593dc153a6fe517657d3f9253b47f8245403780e3135ded836f01aa0624ef9a11a5758c6c2d19a53ce5bb3d42b5144e9a0450ccaa12016c9b2182556a0590de1488f8a8bd085148bab9dbf87c420fe6ef3cc0a1ce2dea26d2d0eecaac7a0fa55108e9b3f33b6b11cddce9a0b5925690767ea712a867a3ae0d216834cee20a02796f3aa663abf0db7ca059053e4c7f030369a1c4d60e370d7af5aa725b87588a098c9b886d05fab70c16d8fd15f2b177d5003340a57721000e7d221c589a8a4a5a091e6663247152b6b3de957f5bd25232b48597c96c840dbf77c997b50d126b3e0a010b9f768eabf65abd237d695268230115ae98bc4c463e19cd30f47487ca679d7a0abfa13187fafd33c5cb0831a09641c1248e9b936731f66947c03f057b0ec3d89a0f295556e04542e999c21f60277e3fcc7e44fcfdf870949e21b846e9742b18b94a02f9affd94353d7652dbd1fcefd9c03a1a9bfc327b236434d1f6c81bf8aef9ac6a0386a1b5d5f2bdc7e93d92e7f11412c113025bad6e33c94ba37510461bf2f94cca08f12924734a19f5728e961a706bc6c0cfda942046e28eb44c1badf297d502bc3a013f93fe1486927a0d10b848bdb442c48705a3b3e7c88aee1d77d78f6d85b1d5ba0d91a18e3c57870ac8b29d43d8a1d3359e279474ea722d7e8e391bdb8c72e11bfa0b08ebbe74c9af83c687d87302e311c702e6f1be26bd7844b7304a685596ede8ea044882f8789a113aef46e82fdf690b2eeb2f1160760ec4e64fe66e47b9df7ea1ba050022023d0bcbb55c76cf23e11b99ee4fca3a3d2b993e45a29ccef01347d163fa0b33522fd871ead1319cfba4bb9876d4308796bb84ffb375cb84c48db2deaf0e0a084a3e4b9f4589095224e4935437234017cda66cd281235626fe61f62e09a235fa0ffbc200c941d48e14c83e6fd9ad02e34e92e4ba020782af80a1739f0c6fbe732a06c55530c2496206713753b0d633f8fa859447978a9edf71c5465eb3f67f20fa4a002e8ce0ea25d482fa460b52bf39015c32f2ad93c7e414a743f8ecd5738388c52a0af5b31ccc42ac9f5c5192989656bdfac5bb3b1d1128ccfef2f5311126e89d5eea06aff3bac0e2c3d89671aaebcad48f94cc2247aa59178155cffd171ce86e9fccca0b81294c4464f0263f982085d672d5253442ad96993d6f835662a55220e8661e6a0834e7a6931833d854821f11d617ab325eebb2c655590e906c019427a4601bab9a051fc387a6cf13f32dd62c7997e14326d373ed2f342bec2ae3b7a9b38ead5e412");
            
            GetReceiptsMessageSerializer serializer = new GetReceiptsMessageSerializer();
            
            GetReceiptsMessage message = serializer.Deserialize(bytes1);
            byte[] serialized = serializer.Serialize(message);
            Assert.AreEqual(bytes1, serialized);
            
            GetReceiptsMessage message2 = serializer.Deserialize(bytes2);
            byte[] serialized2 = serializer.Serialize(message2);
            Assert.AreEqual(bytes2, serialized2);
        }
    }
}
