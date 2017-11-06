using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Providers;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Providers
{
    public class FileUploadProviderTests : BaseAutoMockedTest<FileUploadProvider>
    {
		[Theory, AutoDomainData]
	    public async Task Given_file_and_uniq_id_Upload_should_copy_file_and_should_add_uploaded_data_with_uniq_id(string uniqId)
		{
			string fullPath = "wwwroot/xxx_123.png";
			string uniqFileName = "xxx_123.png";
			string uniqName = "xxx_123";

			var file = GetMock<IFormFile>();
			file.SetupGet(x => x.FileName).Returns("file.png");
			file.Setup(x => x.CopyToAsync(Stream.Null, default(CancellationToken))).Returns(Task.CompletedTask)
;
			var fileInfoFacade = GetMock<IFileInfoFacade>();
			fileInfoFacade.Setup(x => x.GetUniqName()).Returns(uniqName);
			fileInfoFacade.Setup(x => x.GetFilePathWithWeebRoot(uniqFileName)).Returns(fullPath);
			fileInfoFacade.Setup(x => x.GetFileStream(fullPath, FileMode.Create)).Returns(Stream.Null);

			var provider = ClassUnderTest;

			await provider.Upload(file.Object, uniqId);

			Assert.True(provider.HasFile(uniqId));
			Assert.Equal(uniqFileName, provider.GetUploadedFileByUniqId(uniqId));
		}
    }
}
